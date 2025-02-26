﻿using ExtraHours.API.Model;
using ExtraHours.API.Data;
using Microsoft.EntityFrameworkCore;
using ExtraHours.API.Repositories.Interfaces;
using ExtraHours.API.Dto;

namespace ExtraHours.API.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesByManagerIdAsync(long managerId)
        {
            return await _context.employees
                .Include(e => e.manager)
                .Where(e => e.manager != null && e.manager.id == managerId)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(long id)
        {
            return await _context.employees
                .Include(e => e.manager)
                .FirstOrDefaultAsync(e => e.id == id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.employees
                .Include(e => e.manager)
                .ToListAsync();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee != null)
            {
                _context.employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(long id, UpdateEmployeeDTO dto)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
                throw new KeyNotFoundException("Empleado no encontrado");

            employee.name = dto.Name ?? employee.name;
            employee.position = dto.Position ?? employee.position;
            employee.salary = dto.Salary ?? employee.salary;

            if (dto.ManagerId.HasValue)
            {
                var manager = await _context.managers.FindAsync(dto.ManagerId.Value);
                if (manager == null)
                    throw new KeyNotFoundException("Manager no encontrado");
                employee.manager = manager;
            }

            _context.employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
