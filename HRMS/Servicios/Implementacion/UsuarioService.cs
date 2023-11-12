using HRMS.Models;
using HRMS.Servicios.Contrato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HRMS.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HrmssisContext _dbContext;
        public UsuarioService(HrmssisContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetUsuario(string usuario, string clave)
        {
            Role usuario_encontrado = await _dbContext.Roles.Where(u => u.Usuario == usuario && u.Clave == clave)
                 .FirstOrDefaultAsync();

            return usuario_encontrado;
        }

        public async Task<Role> SaveUsuario(Role modelo)
        {
            _dbContext.Roles.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }
    }
}
