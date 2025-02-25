using ExtraHours.API.Model;

namespace ExtraHours.API.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        Task<Manager> GetByIdAsync(long id);
        Task<List<Manager>> GetAllAsync();
        Task<Manager> AddAsync(Manager manager);
        Task<Manager> UpdateAsync(long id, Manager manager);
        Task DeleteAsync(long id);
    }
}
