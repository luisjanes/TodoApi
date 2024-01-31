using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoDbContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Todo> Todos { get; set; }

        public TodoDbContext(IConfiguration configuration)
        {
            _connectionString = configuration
                .GetSection("Database")
                .GetValue<string>("TodoApi");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }
    }
}
