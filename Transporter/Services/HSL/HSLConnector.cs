using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace Transporter.Services.HSL
{
    public interface IHslConnector
    {
        /// <summary>
        /// Example: api.reittiopas.fi/hsl/prod/?request=route&user=<user>&pass=<pass>&format=txt&from=2548196,6678528&to=2549062,6678638
        /// </summary>
        /// <param name="fromCoordinates"></param>
        /// <param name="toCoordinates"></param>
        /// <returns></returns>
        Task<String> GetRoute(string fromCoordinates, string toCoordinates);

        /// <summary>
        /// Example: http://api.reittiopas.fi/hsl/prod/?request=lines&user=<id>&pass=<pw>&format=txt&query=2052%20%201|Tapiola
        /// </summary>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        Task<String> GetLine(string lineCode);
    }

    public class HslConnector : HttpClient, IHslConnector
    {
        private string _password;
        private string _userId;
        private string _baseAddress = "http://api.reittiopas.fi/hsl/prod/";
        private readonly HslSettings _hslSettings;

        public HslConnector(IOptions<HslSettings> hslSettings)
        {
            _hslSettings = hslSettings.Value;
            BaseAddress = new Uri(_baseAddress);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ReadUserIds();
        }

        private void ReadUserIds()
        {
            try
            {
                _userId = _hslSettings.HslUser;
                _password = _hslSettings.HslPassword;

            }
            catch (Exception)
            {
                throw new Exception("Identifikaatiotiedon alustus epäonnistui");

            }
        }

        private string GetIdentityStringForGET()
        {
            return string.Format("user={0}&pass={1}", _userId, _password);
        }

        private string GetFormatForGET()
        {
            return "format=json";
        }

        /// <summary>
        /// Example: api.reittiopas.fi/hsl/prod/?request=route&user=<user>&pass=<pass>&format=txt&from=2548196,6678528&to=2549062,6678638
        /// </summary>
        /// <param name="fromCoordinates"></param>
        /// <param name="toCoordinates"></param>
        /// <returns></returns>
        public async Task<String> GetRoute(string fromCoordinates, string toCoordinates)
        {

            var requestUrl = $"?request=route&{GetFormatForGET()}&{GetIdentityStringForGET()}&from={fromCoordinates}&to={toCoordinates}";
            return await CallHsl(requestUrl);

        }

        /// <summary>
        /// Example: http://api.reittiopas.fi/hsl/prod/?request=lines&user=<id>&pass=<pw>&format=txt&query=2052%20%201|Tapiola
        /// </summary>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public async Task<String> GetLine(string lineCode)
        {
            var requestUrl = $"?request=lines&{GetIdentityStringForGET()}&{GetFormatForGET()}&query={lineCode}";
            return await CallHsl(requestUrl);

        }


        private async Task<string> CallHsl(string requestUrl)
        {
            var response = await GetAsync(requestUrl);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }
    }
} 