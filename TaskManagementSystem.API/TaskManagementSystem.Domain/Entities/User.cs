using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
