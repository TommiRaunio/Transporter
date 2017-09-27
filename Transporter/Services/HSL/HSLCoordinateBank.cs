using System.Collections.Generic;

namespace Transporter.Services.HSL
{
    public class HslCoordinateBank
    {
        private static Dictionary<LocationEnum, string> _coords;
        private static Dictionary<LocationEnum, string> Coords => _coords ?? (_coords = new Dictionary<LocationEnum, string>());

        public static string GetCoordinatesFor(LocationEnum location)
        {
            return Coords[location];
        }

        public static void AddCoordinatesFor(LocationEnum location, string coordinates)
        {
            Coords.Add(location, coordinates);
        }
        
    }
}