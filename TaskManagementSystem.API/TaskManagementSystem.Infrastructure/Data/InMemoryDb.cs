using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Data
{
    public static class InMemoryDb
    {
        public static List<User> Users { get; set; } = new ();
        public static List<TaskItem> TasksItems { get; set; } = new();
    }
}
