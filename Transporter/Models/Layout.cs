using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Models.Pages;

namespace Transporter.Models
{
    public class Layout
    {
        public List<LocationPair> Routes { get; set; }
        public Layout()
        {
            Routes = new List<LocationPair>();
        }

    }

}
