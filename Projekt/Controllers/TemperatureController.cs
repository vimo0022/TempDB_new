using Microsoft.AspNetCore.Mvc;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class TemperatureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTemps()
        {
            List<TemperatureDetaljer> temperatureList = new List<TemperatureDetaljer>();
            TemperatureMetoder tm = new TemperatureMetoder();
            string error = "";
            temperatureList = tm.GetTemperatures(out error);
            ViewBag.Error = error;
            return View(temperatureList);
        }
    }
}
