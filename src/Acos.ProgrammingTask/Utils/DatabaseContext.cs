using Acos.ProgrammingTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Acos.ProgrammingTask.Utils
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base (options) {}

        public DbSet<User> Users { get; set; }
    }
}