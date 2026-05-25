using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagementSystem.Application.DTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
