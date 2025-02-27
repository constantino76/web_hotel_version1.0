using System.ComponentModel.DataAnnotations;

namespace WebHotel_vesion1._0.Models
{
    public class Usuario   
    {
        [RegularExpression("^([0][1-9]-[0-9]{4}-[0-9]{4})|([0-9]-[0-9]{3}-[0-9]{6})$",
           ErrorMessage = "Formato de cedula  invalido Fisica = 0#-####-####   Juridica =#-###-######")]

        public  string IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaActualizacion { get; set; } 
        public string? ImageUrl { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    
        public ICollection<Reserva> Reservas { get; set; } // Relación con Reservas

    }
}
