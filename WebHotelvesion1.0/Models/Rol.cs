
using WebHotel_vesion1._0.Repositories.Interfaces;

namespace WebHotel_vesion1._0.Models
{
    public class Rol:IFechas
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }

        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; } 
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
       
    }
}
