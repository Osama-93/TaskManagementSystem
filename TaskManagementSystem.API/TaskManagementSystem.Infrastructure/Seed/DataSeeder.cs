using System;
using System.Collections.Generic;
using System.Text;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Data;

namespace TaskManagementSystem.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static void Seed()
        {
            if (InMemoryDb.Users.Any())
             return;
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Password = "admin123",
                Role = Role.Admin
            };

            var osama = new User
            {
                Id = Guid.NewGuid(),
                Name = "Osama",
                Password = "user123",
                Role = Role.User
            };

            var ali = new User
            {
                Id = Guid.NewGuid(),
                Name = "Ali",
                Password = "user123",
                Role = Role.User
            };

            var sara = new User
            {
                Id = Guid.NewGuid(),
                Name = "Sara",
                Password = "user123",
                Role = Role.User
            };


            InMemoryDb.Users.Add(admin);
            InMemoryDb.Users.Add(osama);
            InMemoryDb.Users.Add(ali);
            InMemoryDb.Users.Add(sara);

            InMemoryDb.TasksItems.AddRange(new List<TaskItem>
            {
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Finish Backend API",
                    Description = "Complete TasksController endpoints",
                    Status = TaskItemStatus.Open,
                    UserId = osama.Id
                },

                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Angular Navbar",
                    Description = "Create reusable navbar component",
                    Status = TaskItemStatus.InProgress,
                    UserId = osama.Id
                },

                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Fix Authentication",
                    Description = "Debug JWT role issue",
                    Status = TaskItemStatus.Closed,
                    UserId = ali.Id
                },

                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Design Admin Dashboard",
                    Description = "Improve admin UI",
                    Status = TaskItemStatus.Open,
                    UserId = sara.Id
                },

                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Database Migration",
                    Description = "Prepare EF Core migration",
                    Status = TaskItemStatus.InProgress,
                    UserId = admin.Id
                }
            });
            List<User> users = InMemoryDb.Users.ToList();
            
        }
    }
}
