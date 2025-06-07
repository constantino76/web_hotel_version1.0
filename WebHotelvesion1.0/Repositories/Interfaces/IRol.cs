using WebHotel_vesion1._0.Models;

namespace WebHotel_vesion1._0.Repositories.Interfaces
{
    public interface IRol
    {

        public Task<bool> CreateRol(Rol rol);

        public  Task<List<Rol>>  GetRols();
        public Task<bool> UpdateRol(Rol rol);
        public Task<Rol> SearchRol(int id);
        public Task<int> DeleteRol(int rol);
    }
}
