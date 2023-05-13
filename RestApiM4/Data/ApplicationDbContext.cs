using Microsoft.EntityFrameworkCore;
using RestApiM4.Models;

namespace RestApiM4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
