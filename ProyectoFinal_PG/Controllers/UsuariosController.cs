using Microsoft.AspNetCore.Authentication;
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
        [HttpPost]
        public async Task<IActionResult> Registro(DepartamentosCargosViewModel empleados)
        {
            if (!ModelState.IsValid) { return View(empleados); }
            var empleado = new TbEmpleados { EmpleadoNombre1 = empleados.EmpleadoNombre1,
            EmpleadoApellido1 = empleados.EmpleadoApellido1,
            EmpleadoCodigo = empleados.EmpleadoCodigo,
            CargoId = empleados.CargoId};
            
            var resultado = await userManager.
            CreateAsync(empleados, password: empleados.EmpleadoContrasena);
            if (resultado.Succeeded)
            {
               await signInManager.SignInAsync(empleado, isPersistent: true);
               return RedirectToAction("Inicio", "Portal");
                
            }
            else
            {
                
                empleados.Departamentos = await ObtenerDepartamentos();
                empleados.Cargos = await ObtenerCargos(1);
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(empleados);
            }
            
        }
        [AllowAnonymous]
        public IActionResult Logueo()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ValidarDatos()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidarDatos(RecuperarContraseñaModel recuperarContraseña)
        {
            var resultado = await serviciosRegistroLogueo.ValidarDatos(recuperarContraseña);
            




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

        public async Task<IActionResult> Cerrar_sesion()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Logueo", "Usuarios");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logueo(EmpleadoLogueo empleado)
        {
            if (!ModelState.IsValid)
            {
                return View(empleado);
            }

            

            _ = new TbEmpleados();
            TbEmpleados cargoEmpleado = await serviciosRegistroLogueo.BuscarPorCodigoEmpleado(empleado.EmpleadoCodigo);
            var resultado = await signInManager.PasswordSignInAsync(empleado.EmpleadoCodigo,
                empleado.EmpleadoContrasena, empleado.Recuerdame, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                        return RedirectToAction("Inicio", "Portal");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(empleado);
            }
        }
        
    }
}
