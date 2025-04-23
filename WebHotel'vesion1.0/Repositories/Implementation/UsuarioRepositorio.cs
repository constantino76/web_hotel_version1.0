using AppLogin.Data;
using DocumentFormat.OpenXml.Drawing;
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
        // instancia que almacenara el usuario que  se actualizara 
        private static Usuario usuario_;
      

        public UsuarioRepositorio(AppDbContext context) {

            _context = context;
        
            usuario_ = new Usuario();
            
        }
        public async  Task<UserCreationStatus> Create(Usuario usuario)
        { // initial validation by prevent repeat username or password in database
           
      

            try {

                if (_context.Usuarios.Any(x => x.IdUsuario.Equals(usuario.IdUsuario) || x.Correo == usuario.Correo))
                {
                    return UserCreationStatus.DuplicateEmailOrPassword;
                }


                usuario.FechaRegistro = DateTime.Now;
                usuario.FechaActualizacion = null;
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                //return UserCreationStatus.Success;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("Duplicado de clave primaria");

                return UserCreationStatus.ErrorConexionString;
            }


           


            return UserCreationStatus.Success;


        }

        
        public async Task<List<Usuario>> getAll()// retorna todos los usuarios 
        {
            var users = await _context.Usuarios
                  .Include(u => u.UsuarioRoles)        // Incluir la relación UsuarioRoles
                  .ThenInclude(ur => ur.Rol)           // Incluir la información del Rol (nombre, id)
                  .ToListAsync();
            return users;
        }

        //kkk

        public async Task<Usuario> getUser(string id)
        {
            
            try
            {
                if (String.IsNullOrEmpty(id)) {
                    Console.WriteLine("the id don't be  empty or null");
                    return null;
                }
                var  user = _context.Usuarios.Include(e => e.UsuarioRoles).ThenInclude(ur => ur.Rol).Where(u => u.IdUsuario == id).FirstOrDefault();
               //int   result = VerificaPassword(user,  id);
           

                return user;
            }
            catch {


                Console.WriteLine("Error in database query");
            
           
            }
            return null;
        }
        //metodo para verificar email y contrasenia al iniciar sesion 
        public async Task<Usuario> getUserInitSesion(string clave, string email)
        {   
            if (String.IsNullOrEmpty(clave) || String.IsNullOrEmpty(email))
            {


                Console.WriteLine("The key  or  email is null or empty");
                return null;

            }

            try {
               
                var user_ =  _context.Usuarios.Include(e => e.UsuarioRoles).ThenInclude(ur => ur.Rol) .FirstOrDefault(u =>  u.Correo == email&& u.Clave==clave);

                
                return  user_;
                // validamos la contrasenia 
                        }
            catch (DbUpdateException ex) {

                Console.WriteLine("Error in database ");
            
            
            }
            return null;
           
        }

        public  async Task<bool> Update(Usuario usuario)
        { 

            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(usuario.IdUsuario);
                if (usuarioExistente == null)
                {
                    return false; // Si no existe, no se puede actualizar
                }

                _context.Entry(usuarioExistente).CurrentValues.SetValues(usuario);

               await  _context.SaveChangesAsync();

                //_context.Entry(habitacionExistente).CurrentValues.SetValues(habitacionExistente);

                //await _context.SaveChangesAsync()


            }

            catch (DbUpdateException ex ) {
                 
                return false;
            
            
            }
            return  true;
        }

        // metodo para eliminar un usuario en especifico

        public async Task<bool> Delete(string id)
        {
            if (String.IsNullOrEmpty(id)) return false;


            try {

                var userdelete =  _context.Usuarios.FirstOrDefault(e => e.IdUsuario == id);
                if (userdelete!=null) {


                   _context.Usuarios.Remove(userdelete);
                   _context.SaveChanges();
                }
              
            
            }


            catch ( Exception ex) { }


            return true;
        }


        // metodo para verificacion de la contrasenia para cuando va actualizar los datos el usuario


        //public int  VerificaPassword(Usuario usuario,string password) {

        //    var result = passwordhasher.VerifyHashedPassword(usuario, usuario.Clave, password);

        //    if (result != PasswordVerificationResult.Success) //  verifica si la comprobacion ha sido  exitosa 
        //    {
        //        return 0;
        //    }

        //    return 1;
        //}
    }
}
