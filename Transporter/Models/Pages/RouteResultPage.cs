using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.JsonClasses;

namespace Transporter.Models.Pages
{
    public class RouteResultPage: IPage
    {
        public Layout Layout { get; set; }
        public List<HSLRoute> Routes {get; set;}

        public RouteResultPage()
        {
            Routes = new List<HSLRoute>();
        }
    }
}
