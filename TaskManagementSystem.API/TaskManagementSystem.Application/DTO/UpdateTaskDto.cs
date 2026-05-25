using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTO
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskItemStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
