using AppLogin.Data;

using Microsoft.EntityFrameworkCore;
using WebHotel_vesion1._0.Models;

namespace AppLogin.Logica
{
    public class DbLogica
    {
        private readonly AppDbContext _context;
        public DbLogica(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<Usuario>> getAll()
        {
            var users = await _context.Usuarios
                .Include(u => u.UsuarioRoles)        // Incluir la relación UsuarioRoles
                .ThenInclude(ur => ur.Rol)           // Incluir la información del Rol (nombre, id)
                .ToListAsync();
            return users;
        }


        public async Task<Usuario> getUser(Usuario usuario)
        {
            var user = _context.Usuarios.Include(e => e.UsuarioRoles).ThenInclude(ur => ur.Rol).Where(u => u.Clave == usuario.Clave && u.Correo == usuario.Correo).FirstOrDefault();

            return user;

        }



        public async Task<bool> Create(Usuario user) {
            bool status = false;
            try {

                if (user==null) {
                    throw new ArgumentException(nameof(user),"The user is null");
                  
                }
                await _context.Usuarios.AddAsync(user);
                _context.SaveChanges();
                status = true;
            }


            catch (DbUpdateException ex) {

                Console.WriteLine("Error the  conection chain ");
            }
           
            return status;
        
        
        
        
        }
        public async Task<bool> saveUser_Rol(UsuarioRol usuarioRol) {
            try
            {
                _context.UsuarioRols.Add(usuarioRol);
                _context.SaveChanges();
                return true;
            }

            catch (DbUpdateException EX) {

                Console.WriteLine("Error in conectionString  database", EX.ToString());
            
            }

            return false;




        }


        // obtain every roles in the  database
        public async Task<List<Rol>> getRoles() {
           List<Rol>roles = await  _context.Roles.ToListAsync();
            return roles;
            
        
        
        }


    }
}
