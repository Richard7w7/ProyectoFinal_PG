namespace ProyectoFinal_PG.Models
{
    public class empListadoSoli:TbEmpleados
    {
        public IEnumerable<TbSolicitudes> SolicitudesViewModel { get; set; }
    }
}
