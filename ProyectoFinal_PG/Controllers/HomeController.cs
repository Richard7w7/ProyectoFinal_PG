using Microsoft.AspNetCore.Mvc;
using ProyectoFinal_PG.Models;
using ProyectoFinal_PG.Servicios;
using System.Diagnostics;

namespace ProyectoFinal_PG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiciosRegistroLogueo serviciosRegistroLogueo;

        public HomeController(ILogger<HomeController> logger, IServiciosRegistroLogueo serviciosRegistroLogueo)
        {
            _logger = logger;
            this.serviciosRegistroLogueo = serviciosRegistroLogueo;
        }

        public IActionResult Index()
        {
            return View();
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