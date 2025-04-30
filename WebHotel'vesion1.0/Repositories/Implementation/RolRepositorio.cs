using AppLogin.Data;
using Microsoft.EntityFrameworkCore;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Repositories.Implementation
{
    public class RolRepositorio : IRol
    {


        private readonly AppDbContext _context; 
        public RolRepositorio(AppDbContext context)
        {
            _context = context;
        }

            public async Task<bool> CreateRol(Rol rol)
        {  // Validamos que el idRol no exista 
            var rolExistente = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol == rol.IdRol);
            if (rolExistente!=null) { return false; }


           
            try {
                rol.FechaRegistro= DateTime.Now;
                rol.FechaActualizacion = null;

               await  _context.Roles.AddAsync(rol); //add a new rol
               await  _context.SaveChangesAsync();//save  changes
            
            }

            catch (DbUpdateException ex)
            {
         
                Console.WriteLine("Error al guardar cambios en la base de datos: " + ex.Message);
            }
            return true;
        }

        public async Task<List<Rol>> GetRols()
        {
            List<Rol> roles = await _context.Roles.ToListAsync();
            return  roles;
        }

        public async Task<bool> UpdateRol(Rol rol)  
        {
            var rolExistente = await _context.Roles.FirstOrDefaultAsync(r => r.IdRol==rol.IdRol);

            if (rolExistente==null) {

                Console.WriteLine("Este rol no existe en la base de datos ");

                return false;
            }



            try {
                rolExistente.Nombre = rol.Nombre;

                //_context.Roles.Entry(rol).CurrentValues.SetValues(rol);
              int resultado= await  _context.SaveChangesAsync();
            
            }
            catch (DbUpdateException ex) {

                Console.WriteLine("Error al actualizar el registro");
            return false;
            
            }
            return true;
        }

        public async Task<Rol> SearchRol(int id) {

           Rol  rol =await  _context.Roles.FirstOrDefaultAsync(r => r.IdRol == id);


            if (rol==null) { return null; 
            
            
            
            }
            return rol;
        }
        public async Task<int> DeleteRol(int id) {

            var RolDelete =await  _context.Roles.FirstOrDefaultAsync(r => r.IdRol == id);

            if (RolDelete==null) {
                Console.WriteLine("No se encontro el rol");

                return 0;
            
            }
           _context.Roles.Remove(RolDelete);
           int result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
