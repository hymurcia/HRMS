using Microsoft.EntityFrameworkCore;
using HRMS.Models;
using HRMS.Servicios.Implementacion;
namespace HRMS.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Role> GetUsuario(string correo, string v);
        Task<Role> SaveUsuario(Role modelo);
     
    }
}
