using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementSystem.Application.DTO
{
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
