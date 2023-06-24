using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeConnectAPI.Models.BaseModels;
using WeConnectAPI.Models.RoleModels;
using WeConnectAPI.Models.UserModels;

namespace WeConnectAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        // public override int SaveChanges()
        // {
        //     var entities = ChangeTracker.Entries()
        //         .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
        //     foreach (var entity in entities)
        //     {
        //         if (entity.Entity is BaseModel model)
        //         {
        //             if (entity.State == EntityState.Added)
        //             {
        //                 model.CreatedAt = DateTime.Now;
        //             }
        //             model.UpdatedAt = DateTime.Now;
        //         }
        //     }
        //     return base.SaveChanges();
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>()
                .HasOne(u => u.ApplicationUser)
                .WithOne()
                .HasForeignKey<UserProfile>(u => u.ApplicationUserId);
        }
    }
}