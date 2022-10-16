using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoFinal_PG.Models
{
    public class EmpleadoSolicitudesViewModel: TbEmpleados
    {
        public TbSolicitudes SolicitudesViewModelDetalle { get; set; }
        public IEnumerable<SelectListItem> TbEstadosolicitudesView { get; set; }
    }
}
