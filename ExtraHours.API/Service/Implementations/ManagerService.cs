using ExtraHours.API.Model;
using ExtraHours.API.Repositories.Interfaces;
using ExtraHours.API.Service.Interface;
using ExtraHours.API.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExtraHours.API.Service.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<Manager> GetByIdAsync(long id)
        {
            return await _managerRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Manager no encontrado");
        }

        public async Task<List<Manager>> GetAllAsync()
        {
            return await _managerRepository.GetAllAsync();
        }

        public async Task<Manager> AddManagerAsync(Manager manager)
        {
            return await _managerRepository.AddAsync(manager);
        }

        public async Task<Manager> UpdateManagerAsync(long id, UpdateManagerDTO dto)
        {
            var manager = await _managerRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Manager no encontrado");
            manager.name = dto.Name ?? manager.name;
            
            await _managerRepository.UpdateAsync(id, manager);
            return manager;
        }

        public async Task DeleteManagerAsync(long id)
        {
            await _managerRepository.DeleteAsync(id);
        }
    }
}
