using ExtraHours.API.Model;
using ExtraHours.API.Dto;

namespace ExtraHours.API.Service.Interface
{
    public interface IManagerService
    {
        Task<Manager> GetByIdAsync(long id);
        Task<List<Manager>> GetAllAsync();
        Task<Manager> AddManagerAsync(Manager manager);
        Task<Manager> UpdateManagerAsync(long id, UpdateManagerDTO dto);
        Task DeleteManagerAsync(long id);
    }
}
