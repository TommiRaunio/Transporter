using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.JsonClasses;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Transporter.Services.HSL
{
    public interface IHslRouteSolver
    {
        Task<List<HSLRoute>> GetRoute(LocationEnum from, LocationEnum to);
    }

    public class HslRouteSolver : IHslRouteSolver
    {
        //Should handle proper dispose
        private readonly IHslConnector _connector;
        private IMemoryCache _cache;

        public HslRouteSolver(IHslConnector connector, IMemoryCache cache)
        {
            _connector = connector;
            _cache = cache;
        }

        private string CacheKeyFor(LocationEnum from, LocationEnum to)
        {
            return $"{@from}-{to}";
        }

        public async Task<List<HSLRoute>> GetRoute(LocationEnum from, LocationEnum to)
        {
            if (!_cache.TryGetValue(CacheKeyFor(from, to), out List<HSLRoute> listOfRoutes))
            {
                listOfRoutes = await GetRouteFromHsl(from, to);
                _cache.Set(CacheKeyFor(from, to), listOfRoutes, new MemoryCacheEntryOptions()); // Define options
            }            
            return listOfRoutes;

        }

        private async Task<List<HSLRoute>> GetRouteFromHsl(LocationEnum from, LocationEnum to)
        {
            var routeResponse = await _connector.GetRoute(HslCoordinateBank.GetCoordinatesFor(@from), HslCoordinateBank.GetCoordinatesFor(to));
            var listOfRoutes = ParseRouteInformationFromJSON(@from, to, routeResponse);

            if (listOfRoutes != null && listOfRoutes.Any())
            {
                var tasks = new List<Task>();

                listOfRoutes.ForEach(x =>
                {
                        //Launch all the retriaval tasks right after another...
                        //And await with WhenAll a little down further
                        tasks.Add(SetShortCodesForRoute(x));
                });

                //option 2:
                //listOfRoutes.ForEach(async x => {
                //    await SetShortCodesForRoute(x);
                //});

                //option 3:
                //Parallel.ForEach(listOfRoutes, async singleRoute =>
                //    {
                //        await SetShortCodesForRoute(singleRoute);
                //    });

                await Task.WhenAll(tasks);
            }
            return listOfRoutes ?? new List<HSLRoute>();

        }

        private async Task SetShortCodesForRoute(HSLRoute route)
        {

            if (route.legs.Any())
            {
                foreach (var leg in route.legs.Where(x => x.code != null))
                {
                    //the calls to HSL are launched right after another
                    var response = await _connector.GetLine(leg.code);

                    if (!string.IsNullOrEmpty(response))
                    {
                        var fullLineInformation = JsonConvert.DeserializeObject<List<HSLLine>>(response);
                        leg.shortCode = (fullLineInformation.Any()) ? fullLineInformation.First().code_short : "";
                    }


                }
            }
        }

        private List<HSLRoute> ParseRouteInformationFromJSON(LocationEnum from, LocationEnum to, string response)
        {

            List<HSLRoute> listOfRoutes = null;

            try
            {
                if (!string.IsNullOrEmpty(response))
                {

                    var fullReturnedStack = JsonConvert.DeserializeObject<List<List<HSLRoute>>>(response);

                    if (fullReturnedStack.Any())
                    {
                        listOfRoutes = fullReturnedStack.Select(x => x.First()).ToList();
                    }

                }

                return listOfRoutes;

            }
            catch (Exception ex)
            {

                throw new Exception("Kutsu Reittioppaaseen onnistui, mutta vastaus oli odottamatonta formaattia", ex);
            }

        }

    } //class
}