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

            public Task<bool> CreateRol(Rol rol)
        {
            throw new NotImplementedException();
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
