namespace WebHotel_vesion1._0.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }

        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; } = DateTime.UtcNow; 
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
       
    }
}
