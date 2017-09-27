using System.Threading.Tasks;
using Transporter.Models.Pages;
using Transporter.Services;
using Transporter.Services.HSL;
using Microsoft.AspNetCore.Mvc;

namespace Transporter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHslRouteSolver _hslRouteSolver;
        private readonly ILayoutFactory _layoutFactory;

        public HomeController(IHslRouteSolver hslRouteSolver, ILayoutFactory layoutFactory)
        {
            _hslRouteSolver = hslRouteSolver;
            _layoutFactory = layoutFactory;
        }

        public IActionResult Index()
        {
            var homePage = new HomePage();
            homePage.Layout = _layoutFactory.Create();

            return View("~/Views/Pages/Home.cshtml", homePage);
        }
    }
}
