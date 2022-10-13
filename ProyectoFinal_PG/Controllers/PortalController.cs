using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal_PG.Controllers
{
    public class PortalController:Controller
    {
        public PortalController()
        {

        }

        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }
    }
}
