using ExtraHours.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExtraHours.API.Service.Interface;
using ExtraHours.API.Dto;
using ExtraHours.API.Repositories.Interfaces;
using System.Threading.Tasks;
using ExtraHours.API.Repositories.Implementations;


namespace ExtraHours.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IManagerRepository _managerRepository;

        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService, IUserRepository usersRepo, IManagerRepository managerRepository)
        {
            _managerService = managerService;
            _userRepository = usersRepo;
            _managerRepository = managerRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagerById(long id)
        {
            var manager = await _managerService.GetByIdAsync(id);
            if (manager != null)
                return Ok(manager);
            return NotFound(new { error = "manager no encontrado" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _managerService.GetAllAsync();
            return Ok(managers);
        }

        [HttpPost]
        public async Task<IActionResult> AddManager([FromBody] ManagerWithUserDTO dto)
        {
            var manager = new Manager {
                id = dto.Id,
                name = dto.Name
            };

            await _managerService.AddManagerAsync(manager);

            var user = new User
            {
                id = dto.Id,
                email = dto.Name.ToLower().Replace(" ", ".") + "@empresa.com",
                name = dto.Name,
                passwordHash = "password123", // En producción, encriptar
                role = dto.Role ?? "empleado",
                username = dto.Name.ToLower().Replace(" ", ".")
            };

            await _userRepository.SaveAsync(user);
            return Created("", new { message = "Manager y usuario agregados exitosamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(long id, [FromBody] UpdateManagerDTO dto)
        {
            var updatedManager = await _managerService.UpdateManagerAsync(id, dto);
            if (updatedManager == null)
                return NotFound(new { error = "Manager no encontrado" });

            return Ok(new
            {
                message = "Manager actualizado correctamente",
                manager_id = updatedManager.id,
                manager_name = updatedManager.name
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(long id)
        {
            await _managerService.DeleteManagerAsync(id);
            return NoContent();
        }
    }
}
