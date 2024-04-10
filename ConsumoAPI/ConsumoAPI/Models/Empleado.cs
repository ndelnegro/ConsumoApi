namespace ConsumoAPI.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string nombreCompleto {  get; set; }

        public Departamento departamento { get; set; }
       

    }
}
