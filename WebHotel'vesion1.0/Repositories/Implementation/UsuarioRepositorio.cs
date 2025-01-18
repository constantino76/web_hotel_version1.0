using AppLogin.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebHotel_vesion1._0.Enum;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Repositories.Implementation
{
    public class UsuarioRepositorio : IUsuario
    {    // variable of AppDbContext
        private readonly AppDbContext _context;
        //setting password segurity
       // private readonly PasswordHasher<Usuario> _passwordHasher;
        public UsuarioRepositorio(AppDbContext context) {

            _context = context;
           

        }
        public async  Task<UserCreationStatus> Create(Usuario usuario)
        { // initial validation by prevent repeat username or password in database
           
            var hasher = new PasswordHasher<Usuario>();

            try {

                if (_context.Usuarios.Any(x => x.IdUsuario.Equals(usuario.IdUsuario) || x.Correo == usuario.Correo))
                {
                    return UserCreationStatus.DuplicateEmailOrPassword;
                }


                // add encrytation validation 
                // string clave= _passwordHasher.HashPassword(null, usuario.Clave);
                usuario.Clave = hasher.HashPassword(null, usuario.Clave);

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return UserCreationStatus.Success;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("Duplicado de clave primaria");

                return UserCreationStatus.Error;
            }


            catch (DbUpdateException ex) {

                Console.WriteLine("Excepcion  ", ex.ToString());

            }

           
            return UserCreationStatus.Error;


        }

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Usuario>> getAll()
        {
            var users = await _context.Usuarios
                  .Include(u => u.UsuarioRoles)        // Incluir la relación UsuarioRoles
                  .ThenInclude(ur => ur.Rol)           // Incluir la información del Rol (nombre, id)
                  .ToListAsync();
            return users;
        }

        public async Task<Usuario> getUser(string id)
        {
            try
            {
                if (String.IsNullOrEmpty(id)) {
                    Console.WriteLine("the id don't  empty or null");
                    return null;
                }
                var user = _context.Usuarios.Include(e => e.UsuarioRoles).ThenInclude(ur => ur.Rol).Where(u => u.IdUsuario == id).FirstOrDefault();

                return user;
            }
            catch (DbUpdateException ex) {


                Console.WriteLine("Error in database query");
            
           
            }
            return null;
        }

        public async Task<Usuario> getUserInitSesion(string clave, string email)
        {   
            if (String.IsNullOrEmpty(clave) || String.IsNullOrEmpty(email))
            {


                Console.WriteLine("The key  or  email is null or empty");
                return null;

            }

            try {
                //  var   user_ =  _context.Usuarios.Include(e => e.UsuarioRoles).Where(u =>  u.Correo == email);
                var user_ =  _context.Usuarios.Include(e => e.UsuarioRoles).ThenInclude(ur => ur.Rol) .FirstOrDefault(u =>  u.Correo == email &&u.Clave==clave);

             
                return  user_;
                // validamos la contrasenia 
                        }
            catch (DbUpdateException ex) {

                Console.WriteLine("Error in database ");
            
            
            }
            return null;
           
        }

        public Task<bool> Update()
        {
            throw new NotImplementedException();
        }

       
    }
}
