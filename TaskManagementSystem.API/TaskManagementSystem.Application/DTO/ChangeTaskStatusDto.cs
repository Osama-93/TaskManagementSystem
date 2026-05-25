using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTO
{
    public class ChangeTaskStatusDto
    {
        public TaskItemStatus    Status { get; set; }
    }
}
