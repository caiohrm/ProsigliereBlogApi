using Microsoft.EntityFrameworkCore;
using ProsigliereBlogApi.Models;

namespace ProsigliereBlogApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<BlogPost>()
                        .Property(x => x.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
