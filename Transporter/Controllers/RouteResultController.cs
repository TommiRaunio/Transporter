using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporter.Models.Pages;
using Transporter.Services;
using Transporter.Services.HSL;
using Microsoft.AspNetCore.Mvc;


namespace Transporter.Controllers
{
    [Route("HaeReitti")]
    public class RouteResultController : Controller
    {

        private readonly IHslRouteSolver _hslRouteSolver;
        private readonly ILayoutFactory _layoutFactory;

        public RouteResultController(IHslRouteSolver hslRouteSolver, ILayoutFactory layoutFactory)
        {
            _hslRouteSolver = hslRouteSolver;
            _layoutFactory = layoutFactory;
        }

        public async Task<IActionResult> Index(LocationEnum from, LocationEnum to)
        {
            var routes = _hslRouteSolver.GetRoute(from, to);
            var resultPage = new RouteResultPage
            {
                Layout = _layoutFactory.Create()
            };

            resultPage.Routes = await routes;
            return View("~/Views/Pages/RouteResult.cshtml", resultPage);
        }
    }
}
