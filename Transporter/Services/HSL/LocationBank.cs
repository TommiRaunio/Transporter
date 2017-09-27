using System.Collections.Generic;
using System.Linq;
using Transporter.JsonClasses;
using Transporter.Models;

namespace Transporter.Services.HSL
{
    public class LocationBank
    {
        private static Dictionary<LocationEnum, Location> _locations;

        private static Dictionary<LocationEnum, Location> Locations => _locations ?? (_locations = new Dictionary<LocationEnum, Location>());

        public static void Add(LocationEnum location, string friendlyName)
        {
            Locations.Add(location, new Location()
            {
                FriendlyName = friendlyName,
                LocationId = (int)location,
            });
        }

        public static Location Get(LocationEnum location)
        {
            return _locations[location];
        }

        public static List<Location> GetAll()
        {
            return Locations.Values.ToList();
        }
    }
}