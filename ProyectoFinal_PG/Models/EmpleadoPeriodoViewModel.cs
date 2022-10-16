namespace ProyectoFinal_PG.Models
{
    public class EmpleadoPeriodoViewModel:TbEmpleados
    {
        public IEnumerable<TbPeriodos> TbPeriodosView { get; set; }
    }
}
