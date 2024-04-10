using ConsumoAPI.Models;

namespace ConsumoAPI.Services
{
    public interface IServices_Api
    {
        Task<List<Empleado>> Lista();
        Task<Empleado> Obtener(int id);
        Task<bool> Guardar(Empleado objeto);
        Task<bool> Editar(Empleado objeto);
        Task<bool> Eliminar(int id);




    }
}
