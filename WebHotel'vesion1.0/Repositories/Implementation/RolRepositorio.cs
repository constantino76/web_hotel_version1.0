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
        {
            var rolExistente = _context.Roles.FirstOrDefault(r => r.IdRol == rol.IdRol);
            if (rolExistente==null) { return false; }



            try {

                _context.Roles.Add(rol); //add a new rol
                _context.SaveChanges();//save  changes
            
            }

            catch {

                Console.WriteLine("Ha ocurrido un error inesperado en la base de datos");
            
            }
            return true;
        }

        public async Task<List<Rol>> GetRols()
        {
            List<Rol> roles = await _context.Roles.ToListAsync();
            return roles;
        }

        public Task<bool> UpdateRol(Rol rol)  
        {
            throw new NotImplementedException();
        }
    }
}
