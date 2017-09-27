using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transporter.Utils
{
    public static class TransportMethodResolver
    {
        private static readonly string[] _buses = new string[] {"1", "3", "4", "5", "8", "21", "22", "23", "24", "25", "36", "39"};
        private const string _trains = "12";
        private const string _metro = "6";
        private const string _tram = "2";

        public static string GetFriendlyName(string id)
        {
                var typeOfTransport = "";

                if (id == "walk")
                {
                    typeOfTransport = "Kävely";
                }

                if (_buses.Contains(id))
                {
                    typeOfTransport = "Bussi";
                }

                if (id == _trains)
                {
                    typeOfTransport = "Juna";
                }

                if (id == _metro)
                {
                    typeOfTransport = "Metro";
                }

                if (id == _tram)
                {
                    typeOfTransport = "Raitiovaunu";
                }

                return typeOfTransport;
        }
    }
}
