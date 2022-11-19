
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal_PG.Models;
using ProyectoFinal_PG.Servicios;
using Syncfusion.Pdf.Parsing;
using System.Globalization;

namespace ProyectoFinal_PG.Controllers
{
    public class PortalController : Controller
    {

        private readonly IServicioEmpleados servicioEmpleados;
        private readonly IServiciosSolicitudes serviciosSolicitudes;
        private readonly IServiciosRegistroLogueo serviciosRegistro;
        

        public PortalController(IServicioEmpleados servicioEmpleados,
            IServiciosSolicitudes serviciosSolicitudes, 
            IServiciosRegistroLogueo serviciosRegistro)
        {

            this.servicioEmpleados = servicioEmpleados;
            this.serviciosSolicitudes = serviciosSolicitudes;
            this.serviciosRegistro = serviciosRegistro;
            
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
        public async Task<IActionResult> FiltrarporAprobado()
        {
            var modelo = new empListadoSoli();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            int id = (int)empleado.EmpleadoId;
            modelo.SolicitudesViewModel = await serviciosSolicitudes.ListadoSolicitudesEmpleadoAprobadas(id);
            modelo.cargoNombre = empleado.Cargo.CargoNombre;
            return View("ListadeSolicitudes", modelo);
        }
        [HttpGet]
        public async Task<IActionResult> FiltrarporDenegado()
        {
            var modelo = new empListadoSoli();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            int id = (int)empleado.EmpleadoId;
            modelo.SolicitudesViewModel = await serviciosSolicitudes.ListadoSolicitudesEmpleadoDenegadas(id);
            modelo.cargoNombre = empleado.Cargo.CargoNombre;
            return View("ListadeSolicitudes",modelo);
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
            var haysoli = await serviciosSolicitudes.BuscarsolicitudesconEstadoEnviado(idemp);
            empleadomodel = await serviciosSolicitudes.ObtenerPeriodosPorId(idemp);

            ViewBag.Periodo = empleadomodel.Periodo;
            ViewBag.Dias = empleadomodel.cantidad_dias;
            ViewBag.Idemp = haysoli;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearSolicitud(TbSolicitudes modelo)
        {
            var empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            var cantidadDiasVacacionales = await serviciosSolicitudes
                .ObtenerPeriodosPorId((int)empleado.EmpleadoId);
            string[] cantidadDias = modelo.SolicitudFechasSeleccionadas.Split(',');
            ViewBag.Dias = cantidadDiasVacacionales.cantidad_dias;
            int dias = cantidadDias.Length;
            int diaslimites = (int)ViewBag.Dias;
            if(modelo.TiposolicitudId == (int)EnumtipoSolicitud.Vacacional && modelo.SolicitudPeriodoVacas == null)
            {
                ViewBag.Solicitud = "SinPeriodo";
                return View();
            }
            if(modelo.TiposolicitudId == (int)EnumtipoSolicitud.Licencia || modelo.TiposolicitudId == (int)EnumtipoSolicitud.Permiso)
            {
                ViewBag.Periodo = cantidadDiasVacacionales.Periodo;
                ViewBag.Dias = cantidadDiasVacacionales.cantidad_dias;
                modelo.solicitud_depto_Id = empleado.DeptoId;
                var idsoli = await serviciosSolicitudes.CrearSolicitud(modelo);
                ViewBag.Periodo = cantidadDiasVacacionales.Periodo;
                ViewBag.Dias = cantidadDiasVacacionales.cantidad_dias;
                ViewBag.Solicitud = "creada";

                return RedirectToAction("ListadeSolicitudes");

            }
            if ( dias > diaslimites)
            {
                
                   
                ViewBag.Periodo = cantidadDiasVacacionales.Periodo;
                ViewBag.Dias = cantidadDiasVacacionales.cantidad_dias;
                ViewBag.Solicitud = "limite";
                return View();
            }
            if (!ModelState.IsValid)
            {
                
                ViewBag.Periodo = cantidadDiasVacacionales.Periodo;
                ViewBag.Dias = cantidadDiasVacacionales.cantidad_dias;
                return View();
            }
            
            modelo.solicitud_depto_Id = empleado.DeptoId;
            var id = await serviciosSolicitudes.CrearSolicitud(modelo);
            ViewBag.Periodo = cantidadDiasVacacionales.Periodo;
            ViewBag.Dias = cantidadDiasVacacionales.cantidad_dias;
            ViewBag.Solicitud = "creada";
            
            return RedirectToAction("ListadeSolicitudes");

        }

        public async Task<IActionResult> ListarSolicitudesDepartamento()
        {
            var solicitudes = new empListadoSoli();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            solicitudes.SolicitudesViewModel = await serviciosSolicitudes
                .ListadoSolicitudesDepartamento(empleado.DeptoId,empleado);
            solicitudes.cargoNombre = empleado.Cargo.CargoNombre;
            return View("SolicitudesDepartamento",solicitudes);
        }

        public async Task<IActionResult> ListarSolicitudesdeTodosDepartamentosparaAprobarDenegar()
        {
            var solicitudes = new empListadoSoli();
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            solicitudes.SolicitudesViewModel = await serviciosSolicitudes
                .ListarSolicitudesdeTodosDepartamentosparaAprobarDenegar();
            solicitudes.cargoNombre = empleado.Cargo.CargoNombre;
            return View("SolicitudesDepartamento", solicitudes);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarSolicitudAsync(EmpleadoSolicitudesViewModel modelo)
        {
            var solicitud = modelo.SolicitudesViewModelDetalle;
            var seModifico = await serviciosSolicitudes.ModificarEstadoSolicitud(solicitud);
            if(!seModifico) { return NotFound(); }
            return RedirectToAction("ListarSolicitudesDepartamento");
            
        }

        [HttpGet]
        public async Task<IActionResult> Perfil()
        {

            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
            return View(empleado);
        }

        [HttpGet]
        public IActionResult ModificarPerfil()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModificarPerfil(ActualizaDatos actualizaDatos)
        {
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await serviciosSolicitudes.ActualizarDatos(actualizaDatos,empleadoId);
            if(empleado != false) { 
            return RedirectToAction("Perfil");
            }
            return RedirectToAction("Perfil");
        }

        public IActionResult AgregarDepartamentos()
        {
            return View();
        }

        public async Task<IActionResult> AgregarCargos()
        {
            var modelo = new AgregarCargosViewModel
            {
                tbDepartamentosSelect = await ObtenerDepartamentos()
            };
            return View(modelo);
        }
        private async Task<IEnumerable<SelectListItem>> ObtenerDepartamentos()
        {
            var departamentos = await serviciosRegistro.ObtenerDepartamentos();
            return departamentos.Select(x => new SelectListItem(x.DeptoNombre, x.DeptoId.ToString()));
        }
        public IActionResult AgregarCodigosdeEmpleado()
        {
            return View();
        }
        public async Task<IActionResult> AgregarPeriodos()
        {
            var modelo = new AgregarPeriodosViewModel
            {
                tbempleadosView = await ObtenerEmpleados()
            };
            return View(modelo);
            
        }
        [HttpPost]
        public async Task<IActionResult> AgregarDepartamentos(AgregarDepartamentos departamentos)
        {
            if(departamentos == null)
            {
                ViewBag.Departamento = "NO";
                return View();
            }
            var agrego = await serviciosSolicitudes.InsertarDepartamento(departamentos);
            if(agrego != false)
            {
                ViewBag.Departamento = "SI";
                return View();
            }
            ViewBag.Departamento = "NO";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AgregarCargos(AgregarCargosViewModel cargos)
        {
            var modelo = new AgregarCargosViewModel
            {
                tbDepartamentosSelect = await ObtenerDepartamentos()
            };
            if (cargos == null)
            {
                ViewBag.Cargo = "NO";
                return View(modelo);
            }
            var resultado = await serviciosSolicitudes.InsertarCargos(cargos);
            if (resultado != false)
            {
                ViewBag.Cargo = "SI";
                modelo.CargoDescripcionView = null;
                modelo.CargoNombreView = null;
                return View(modelo);
            }
            ViewBag.Cargo = "NO";
            return View(modelo);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarCodigosdeEmpleado(AgregarCodigosdeEmpleado codigoEmpleado)
        {
            if (codigoEmpleado== null)
            {
                ViewBag.Codigo= "NO";
                return View();
            }
            var agrego = await serviciosSolicitudes.InsertarCodigo(codigoEmpleado);
            if (agrego != false)
            {
                ViewBag.Codigo = "SI";
                return View();
            }
            ViewBag.Codigo = "NO";
            return View();
            
        }
        [HttpPost]
        public async  Task<IActionResult> AgregarPeriodos(AgregarPeriodosViewModel agregarPeriodos)
        {
            var modelo = new AgregarPeriodosViewModel
            {
                tbempleadosView = await ObtenerEmpleados()
            };
            if (agregarPeriodos.periodo_vacacionalView == null)
            {
                ViewBag.Periodo = "NO";
                return View(modelo);
            }
            var resultado = await serviciosSolicitudes.InsertarPeriodo(agregarPeriodos);
            if (resultado != false)
            {
                ViewBag.Periodo = "SI";
                modelo.periodo_cantidad_diasView = 0;
                modelo.periodo_vacacionalView = null;
                modelo.periodo_observacionesView = null;
                modelo.tbempleadosView = await ObtenerEmpleados();
                return View(modelo);
            }
            ViewBag.Periodo = "NO";
            return View(modelo);
           
            
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerEmpleados()
        {
            var empleados = await serviciosRegistro.ObtenerEmpleados();
            return empleados.Select(x => new SelectListItem(x.EmpleadoCodigo, x.EmpleadoId.ToString()));
        }

        //Crear archivo
        public async Task<IActionResult> GenerarTemplatePDF(int id)
        {
           
                var modelo = new EmpleadoPeriodoViewModel();
                int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
                var empleado = await serviciosSolicitudes.BuscarEmpleadoporCodigo(empleadoId);
                int idemp = (int)empleado.EmpleadoId;
                var tbsolicitudes = await serviciosSolicitudes.ObtenerSolicitudDetalle(id, idemp);

                //FileStream pdfStream = new FileStream(@"wwwroot\Template\Template.pdf", FileMode.Open, FileAccess.Read);
                FileStream pdfStream = new FileStream(@"wwwroot/Template/Template.pdf", FileMode.Open, FileAccess.Read);
                PdfLoadedDocument cargaDoc = new PdfLoadedDocument(pdfStream);
                PdfLoadedForm formuPdf = cargaDoc.Form;

                (formuPdf.Fields["txtFecha"] as PdfLoadedTextBoxField).Text = tbsolicitudes.SolicitudFecha.ToShortDateString();
                (formuPdf.Fields["txttipoSoli"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Tiposolicitud.TiposolicitudNombre;
                (formuPdf.Fields["txtCodigoEmpleado"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.EmpleadoCodigo;
                (formuPdf.Fields["txtNombre"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.EmpleadoNombre1 + " " + tbsolicitudes.Empleado.EmpleadoApellido1;
                (formuPdf.Fields["txtDireccion"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.Depto.DeptoNombre;
                (formuPdf.Fields["txtPuesto"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.Cargo.CargoNombre;
                (formuPdf.Fields["txtInicioLaboral"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.FechaIngresoLaboral.ToShortDateString();
                (formuPdf.Fields["txtCantiDias"] as PdfLoadedTextBoxField).Text = tbsolicitudes.SolicitudCantidadDias.ToString();
                if (tbsolicitudes.SolicitudPeriodoVacas == null)
                {
                    (formuPdf.Fields["txtCorrespondePeriodo"] as PdfLoadedTextBoxField).Text = "";
                }
                else
                {
                    (formuPdf.Fields["txtCorrespondePeriodo"] as PdfLoadedTextBoxField).Text = tbsolicitudes.SolicitudPeriodoVacas;
                }
                string[] arrayfechas = tbsolicitudes.SolicitudFechasSeleccionadas.Split(',');
                (formuPdf.Fields["txtInicioVacas"] as PdfLoadedTextBoxField).Text = arrayfechas[0];
                (formuPdf.Fields["txtFinVacas"] as PdfLoadedTextBoxField).Text = arrayfechas.Last();
                var inicioLaboral = arrayfechas.Last();
            string[] fechas = inicioLaboral.Split("/");
            int dia = int.Parse(fechas[0]);
            dia += 1;
            string fechafinal = dia +"/"+ fechas[1] + "/"+fechas[2];
                (formuPdf.Fields["txtRegresoLaboral"] as PdfLoadedTextBoxField).Text = fechafinal;
                if (tbsolicitudes.SolicitudComentario == null)
                {
                    (formuPdf.Fields["txtComentario"] as PdfLoadedTextBoxField).Text = "";
                }
                else
                {
                    (formuPdf.Fields["txtComentario"] as PdfLoadedTextBoxField).Text = tbsolicitudes.SolicitudComentario;
                }

            (formuPdf.Fields["txtdetalles"] as PdfLoadedTextBoxField).Text = tbsolicitudes.SolicitudDetalles;
                string[] arrayEstadoRRHH = tbsolicitudes.SolicitudEstadoRrHh.Split(' ');
                (formuPdf.Fields["txtEstado"] as PdfLoadedTextBoxField).Text = arrayEstadoRRHH[0];
                (formuPdf.Fields["txtNotificado1"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.EmpleadoNombre1 + " " + tbsolicitudes.Empleado.EmpleadoApellido1; ;
                (formuPdf.Fields["txtNotificado2"] as PdfLoadedTextBoxField).Text = tbsolicitudes.Empleado.Cargo.CargoNombre;
                string[] arrayEstadoJefe = tbsolicitudes.SolicitudEstadoSeleJefe.Split(' ');
                (formuPdf.Fields["txtEstadoJefeInmediato"] as PdfLoadedTextBoxField).Text = arrayEstadoJefe[0];
                (formuPdf.Fields["txtNombreJefeInmediato"] as PdfLoadedTextBoxField).Text = arrayEstadoJefe[1] + " " + arrayEstadoJefe[2];
                (formuPdf.Fields["txtPuestoJefeInmediato"] as PdfLoadedTextBoxField).Text = "Jefe Inmediato de " + tbsolicitudes.Empleado.Depto.DeptoNombre;
                string[] arrayEstadoDirector = tbsolicitudes.SolicitudEstadoDirector.Split(' ');
                (formuPdf.Fields["txtEstadoDirector"] as PdfLoadedTextBoxField).Text = arrayEstadoDirector[0];
                (formuPdf.Fields["txtNombreDirector"] as PdfLoadedTextBoxField).Text = arrayEstadoDirector[1] + " " + arrayEstadoDirector[2];
                (formuPdf.Fields["txtPuestoDirector"] as PdfLoadedTextBoxField).Text = "Director de " + tbsolicitudes.Empleado.Depto.DeptoNombre;

                (formuPdf.Fields["txtestadoRRHH"] as PdfLoadedTextBoxField).Text = arrayEstadoRRHH[0];
                (formuPdf.Fields["txtNombreRRHH"] as PdfLoadedTextBoxField).Text = arrayEstadoRRHH[1] + " " + arrayEstadoRRHH[2];

                MemoryStream stream = new MemoryStream();
                cargaDoc.Save(stream);
                stream.Position = 0;
                cargaDoc.Close(true);
                string contentType = "application/pdf";
                string fileName = "Solicitud #" + tbsolicitudes.SolicitudId + " " + tbsolicitudes.Empleado.Depto.DeptoNombre + " " + tbsolicitudes.Empleado.EmpleadoCodigo + ".pdf";

                return File(stream, contentType, fileName);
            
            
        }

       
       
    }
}
