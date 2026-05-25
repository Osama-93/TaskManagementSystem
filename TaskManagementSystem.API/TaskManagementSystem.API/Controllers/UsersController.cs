using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTO;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        
        public IActionResult GetUsers()
        {
            var users = InMemoryDb.Users;
            return Ok(users);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            var user = InMemoryDb.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser(CreateUserDto dto)
        {
            var userExists = InMemoryDb.Users.Any(u => u.Name == dto.Username);
            if (userExists)
            {
                return BadRequest("UserName already exists");
            }

            var user = new Domain.Entities.User
            {
                Name = dto.Username,
                Password = dto.Password,
                Role = Enum.Parse<Domain.Enums.Role>(dto.Role)
            };

            InMemoryDb.Users.Add(user);

            return Ok(new { Message = "User created successfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto dto)
        {
            var user = InMemoryDb.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Name = dto.Username;
            user.Password = dto.Password;
            user.Role = Enum.Parse<Domain.Enums.Role>(dto.Role);

            return Ok("User updated successfully");
        }
    }
}
