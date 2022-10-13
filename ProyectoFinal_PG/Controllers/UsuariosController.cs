using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal_PG.Models;
using ProyectoFinal_PG.Servicios;

namespace ProyectoFinal_PG.Controllers
{
    public class UsuariosController: Controller
    {
        private readonly UserManager<TbEmpleados> userManager;
        private readonly SignInManager<TbEmpleados> signInManager;
        private readonly IServiciosRegistroLogueo serviciosRegistroLogueo;

        public UsuariosController( UserManager<TbEmpleados> userManager, SignInManager<TbEmpleados> signInManager,
            IServiciosRegistroLogueo serviciosRegistroLogueo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.serviciosRegistroLogueo = serviciosRegistroLogueo;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Registro()
        {
            var modelo = new DepartamentosCargosViewModel();
            modelo.Departamentos = await ObtenerDepartamentos();
            modelo.Cargos = await ObtenerCargos(1);
            return View(modelo);
        }
        [AllowAnonymous]
        public IActionResult Logueo()
        {
            return View();
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerDepartamentos()
        {
            var departamentos = await serviciosRegistroLogueo.ObtenerDepartamentos();
            return departamentos.Select(x => new SelectListItem(x.DeptoNombre, x.DeptoId.ToString()));
        }
        [AllowAnonymous]
        private async Task<IEnumerable<SelectListItem>> ObtenerCargos(int deptoId)
        {
            var cargos = await serviciosRegistroLogueo.ObtenerCargos(deptoId);
            return cargos.Select(x => new SelectListItem(x.CargoNombre, x.CargoId.ToString()));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ObtenerCargosSelect([FromBody] int deptoId)
        {
            var cargos = await ObtenerCargos(deptoId);
            return Ok(cargos);
        }
       
    }
}
