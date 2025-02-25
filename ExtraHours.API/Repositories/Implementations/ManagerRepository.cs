using ExtraHours.API.Model;
using ExtraHours.API.Data;
using Microsoft.EntityFrameworkCore;
using ExtraHours.API.Repositories.Interfaces;
using ExtraHours.API.Dto;

namespace ExtraHours.API.Repositories.Implementations
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _context;

        public ManagerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Manager?> GetByIdAsync(long id)
        {
            return await _context.managers
                .Include(e => e.id)
                .FirstOrDefaultAsync(e => e.id == id);
        }

        public async Task<List<Manager>> GetAllAsync()
        {
            return await _context.managers
                .ToListAsync();
        }

        public async Task<Manager> AddAsync(Manager manager)
        {
            await _context.managers.AddAsync(manager);
            await _context.SaveChangesAsync();
            return manager;
        }

        public async Task UpdateAsync(Manager manager)
        {
            _context.managers.Update(manager);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var manager = await _context.managers.FindAsync(id);
            if (manager != null)
            {
                _context.managers.Remove(manager);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("manager no encontrado");
            }
        }

        public async Task<Manager> UpdateAsync(long id, Manager manager)
        {
            _context.managers.Update(manager);
            await _context.SaveChangesAsync();
            return manager;
        }

    }
}
