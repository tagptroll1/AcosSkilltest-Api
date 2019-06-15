using Acos.ProgrammingTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Acos.ProgrammingTask.Utils
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Whiteboard> Whiteboards { get; set; }
        public DbSet<Postit> Postits { get; set; }
        public DbSet<Todo> Todos {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postit>()
                .HasOne( p => p.Todo)
                .WithOne( t => t.Postit)
                .HasForeignKey<Todo>( t => t.PostitForeignKey);

            modelBuilder.Entity<Whiteboard>()
                .HasOne(w => w.User)
                .WithMany(u => u.Whiteboards)
                .IsRequired();

            modelBuilder.Entity<Postit>()
                .HasOne(x => x.Whiteboard)
                .WithMany(x => x.Postits);
        }
    }
}