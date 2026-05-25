using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTO;
using TaskManagementSystem.Infrastructure.Data;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateTask(CreateTaskDto dto)
        {
            var newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                UserId = dto.UserId
            };
            InMemoryDb.TasksItems.Add(newTask);
            return Ok(newTask);
        }
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var role = GetCurrentUserRole();
            if(role != Role.Admin.ToString())
            {
                return Forbid();
            }
            return Ok(InMemoryDb.TasksItems);
        }
        [HttpGet("my-tasks")]
        public IActionResult GetMyTasks()
        {
            var currentUserId = GetCurrentUserId();

            var tasks = InMemoryDb.TasksItems.Where(t => t.UserId == currentUserId).ToList();

            var results = tasks.Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.UserId,

                UserName = InMemoryDb.Users.FirstOrDefault(u => u.Id == t.UserId)
            });
            return Ok(tasks);
        }

        [HttpPatch("{id}/status")]
        public IActionResult ChangeTaskStatus(Guid id, ChangeTaskStatusDto dto)
        {
            var task = InMemoryDb.TasksItems.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                return NotFound("Task not found");
            }

            var currentUserId = GetCurrentUserId();
            var currentRole = GetCurrentUserRole();

            bool isOwner = task?.UserId == currentUserId;
            bool isAdmin = currentRole == Role.Admin.ToString();

            if (!isOwner && !isAdmin)
            {
                return Forbid();
            }

            task.Status = dto.Status;
            return Ok(task);
        }
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Welcome, Admin!");
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateTask(Guid Id, UpdateTaskDto dto)
        {
            var task = InMemoryDb.TasksItems.FirstOrDefault(t => t.Id == Id);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            var userId = User.FindFirst("UserId")?.Value;

            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Admin" && task.UserId.ToString() != userId)
            {
                return Forbid();
            }

            task.Title = dto.Title;

            task.Description = dto.Description;

            task.Status = dto.Status;

            task.UserId = dto.UserId;

            return Ok(task);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(Guid id)
        {
            var task = InMemoryDb.TasksItems.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            var currentUserId = GetCurrentUserId();

            var currentRole = GetCurrentUserRole();

            bool isOwner = task.UserId == currentUserId;

            bool isAdmin = currentRole == Role.Admin.ToString();

            if (!isOwner && !isAdmin)
            {
                return Forbid();
            }

            InMemoryDb.TasksItems.Remove(task);

            return Ok(new { message = "Task deleted successfully" });
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }
        private string GetCurrentUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value!;
        }
    }

}


