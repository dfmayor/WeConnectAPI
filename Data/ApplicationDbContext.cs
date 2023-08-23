using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeConnectAPI.Models;
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
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GigReview> GigReviews { get; set; }
        public DbSet<GigModel> GigModels { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderModel> OrderModels { get; set; }
        public DbSet<Education> Educations { get; set; }

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

            modelBuilder.Entity<GigReview>()
                .HasKey(gr => gr.GigReviewId);

            modelBuilder.Entity<GigReview>()
                .HasOne(gr => gr.Gig)
                .WithMany(g => g.GigReviews)
                .HasForeignKey(gr => gr.GigId);

            modelBuilder.Entity<GigReview>()
                .HasOne(gr => gr.Review)
                .WithMany(r => r.GigReviews)
                .HasForeignKey(gr => gr.ReviewId);
            
            modelBuilder.Entity<GigModel>()
                .Property(g => g.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Review>()
                .Property(r => r.Rating)
                .HasDefaultValue(0.0);
        }
    }
}