using AppLogin.Data;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Repositories.Implementation
{
    public class UsuarioRolRepositorio:IUsuarioRol
    {  private readonly AppDbContext _context;
        public UsuarioRolRepositorio(AppDbContext context) { 
        
        _context= context;  
        
        }
        public async Task<bool> InsertUserRol(UsuarioRol usuariorol) {

            var user = new UsuarioRol { 
            IdRol = usuariorol.IdRol,   
            IdUsuario= usuariorol.IdUsuario,    
            
            };


            _context.UsuarioRols.AddAsync(user);
            _context.SaveChanges();
            return true;
        
        
        }
    }
}
