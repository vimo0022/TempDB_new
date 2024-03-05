using Microsoft.AspNetCore.Mvc;
using Projekt.Models;
using System.Diagnostics;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            TemperatureMetoder tm = new TemperatureMetoder();
            string error = "";
            TemperatureDetaljer latestTemperature = tm.GetLatestTemperature(out error);
            ViewBag.Error = error;
            return View(latestTemperature);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
