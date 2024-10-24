using Microsoft.EntityFrameworkCore;
using Task_managment_system.Models;

namespace Task_managment_system.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options):base(options)
        {
          
        }

        public DbSet<Atask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
