using AppLogin.Data;
using Microsoft.EntityFrameworkCore;
using WebHotel_vesion1._0.Models;
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Repositories.Implementation
{
    public class HabitacionRepositorio : IHabitacion

    {
        private readonly AppDbContext _context;

        public HabitacionRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearHabitacion(Habitacion habitacion)
        {
            try {
                _context.Habitacion.Add(habitacion);
                _context.SaveChanges();  
            }



            catch { }   
        }

        public async Task<List<Habitacion>> ListarHabitaciones()
        {
            var habitaciones =await  _context.Habitacion.ToListAsync();  
            return habitaciones;    
        }
    }
}
