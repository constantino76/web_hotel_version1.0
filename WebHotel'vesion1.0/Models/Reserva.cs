namespace WebHotel_vesion1._0.Models
{
    public class Reserva
    {

        public int Id { get; set; }
        public DateTime FechaReserva { get; set; } // Fecha en que se realiza la reserva
        public string UsuarioId { get; set; } // Cliente que reserva
        public Usuario Usuario { get; set; } // Relación con Usuario
        public int HabitacionId { get; set; } // Habitación reservada
        public Habitacion Habitacion { get; set; } // Relación con Habitación
        public string MetodoPago { get; set; } // Ejemplo: "Tarjeta" o "Físico"
        public bool Confirmado { get; set; } // Estado de confirmación de la reserva
    }
}
