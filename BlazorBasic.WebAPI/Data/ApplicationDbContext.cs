using BlazorBasic.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorBasic.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }

    }
}
