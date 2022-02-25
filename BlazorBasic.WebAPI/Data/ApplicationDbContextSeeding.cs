using Blazorbasic.Models.Enums;
using BlazorBasic.WebAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace BlazorBasic.WebAPI.Data
{
    public class ApplicationDbContextSeeding
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public ApplicationDbContextSeeding(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher =  new PasswordHasher<User>();
        }
        public async Task SeedAsync()
        {
            if (!_context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "032132131",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                _context.Users.Add(user);
            }
            if (!_context.Tasks.Any())
            {
                _context.Tasks.Add(new Entities.Task()
                {
                    Guid = Guid.NewGuid(),
                    Name = "Same Task 1",
                    CreatedDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
