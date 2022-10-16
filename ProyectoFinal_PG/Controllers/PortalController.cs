using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal_PG.Models;
using ProyectoFinal_PG.Servicios;

namespace ProyectoFinal_PG.Controllers
{
    public class PortalController : Controller
    {

        private readonly IServicioEmpleados servicioEmpleados;
        private readonly IServiciosSolicitudes serviciosSolicitudes;

        public PortalController(IServicioEmpleados servicioEmpleados,
            IServiciosSolicitudes serviciosSolicitudes)
        {

            this.servicioEmpleados = servicioEmpleados;
            this.serviciosSolicitudes = serviciosSolicitudes;
        }
        private async Task<IEnumerable<SelectListItem>> ObtenerTiposSolicitudes()
        {
            var tiposSolicitudes = await serviciosSolicitudes.ObtenerTiposSolicitudes();
            return tiposSolicitudes.Select(x =>
            new SelectListItem(x.TiposolicitudNombre, x.TiposolicitudId.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult> Inicio()
        {
            var empleado = new TbEmpleados();
            var empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);

            return View(empleado);
        }
        public async Task<IActionResult> DetallesSolicitud(int? id)
        {
               
            if (id is null)
            {
                return View();
            }
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            int idemp = (int)empleado.EmpleadoId;
            var modelo = new EmpleadoSolicitudesViewModel();
            modelo.SolicitudesViewModelDetalle = await serviciosSolicitudes.ObtenerSolicitudDetalle(id, idemp);
            return View(modelo);
        }

        public async Task<IActionResult> ModificarSolicitud(int id)
        {
            var solicitudAModificar = new EmpleadoSolicitudesViewModel();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);

            solicitudAModificar.SolicitudesViewModelDetalle = await serviciosSolicitudes.DetalleSolicitudDepartamento(id, empleado);
            solicitudAModificar.TbEstadosolicitudesView = await ObtenerEstados();
            return View(solicitudAModificar);
            
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerEstados()
        {
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            var estados = await serviciosSolicitudes.ObtenerEstadosSolicitudes(empleado);
            return estados.Select(x => new SelectListItem(x.EstadosNombre, x.EstadosolicitudId.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> ListadeSolicitudes()
        {
            var modelo = new empListadoSoli();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            int id = (int)empleado.EmpleadoId;
            modelo.SolicitudesViewModel = await serviciosSolicitudes.ListadoSolicitudesEmpleadoId(id);
            modelo.cargoNombre = empleado.Cargo.CargoNombre;
            return View(modelo);
        }
        [HttpGet]
        public async Task<IActionResult> PeriodosVacacionales()
        {
            var modelo = new EmpleadoPeriodoViewModel();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            int idemp = (int)empleado.EmpleadoId;
            modelo.TbPeriodosView = await serviciosSolicitudes.ObtenerPeriodosEmpleadoId(idemp);

            return View(modelo);

        }


        [HttpGet]
        public async Task<ActionResult> CrearSolicitud()
        {
            var empleadomodel = new TbEmpleados();

            var empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            int idemp = (int)empleado.EmpleadoId;
            empleadomodel = await serviciosSolicitudes.ObtenerPeriodosPorId(idemp);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearSolicitud(TbSolicitudes modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            modelo.solicitud_depto_Id = empleado.DeptoId;
            var id = await serviciosSolicitudes.CrearSolicitud(modelo);
            
            return View();

        }

        public async Task<IActionResult> ListarSolicitudesDepartamento()
        {
            var solicitudes = new empListadoSoli();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            solicitudes.SolicitudesViewModel = await serviciosSolicitudes
                .ListadoSolicitudesDepartamento(empleado.DeptoId,empleado);
            
            return View("SolicitudesDepartamento",solicitudes);
        }

        [HttpPost]
        public IActionResult ModificarSolicitud(EmpleadoSolicitudesViewModel modelo)
        {
            var solicitud = modelo.SolicitudesViewModelDetalle;
            var seModifico = serviciosSolicitudes.ModificarEstadoSolicitud(solicitud).Result;
            if(!seModifico) { return NotFound(); }
            return RedirectToAction("ListarSolicitudesDepartamento");
            
        }


    }
}
