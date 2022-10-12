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
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IServiciosRegistroLogueo serviciosRegistroLogueo;

        public UsuariosController( UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
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
        
        private async Task<IEnumerable<SelectListItem>> ObtenerCargos(TbDepartamentosLaborales deptoId)
        {
            var cargos = await serviciosRegistroLogueo.ObtenerCargos(deptoId);
            return cargos.Select(x => new SelectListItem(x.CargoNombre, x.CargoId.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCargosSelect([FromBody] TbDepartamentosLaborales deptoId)
        {
            var cargos = await ObtenerCargos(deptoId);
            return Ok(cargos);
        }
    }
}
