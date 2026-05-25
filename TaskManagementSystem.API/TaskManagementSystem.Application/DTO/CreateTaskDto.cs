using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTO
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public TaskItemStatus Status { get; set; }
    }
}
