using Microsoft.EntityFrameworkCore;
using JobseekBerca.Models;
using System.Data;

namespace JobseekBerca.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Applications> Applications { get; set; }


        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<SocialMedias> SocialMedias { get; set; }
        public DbSet<SavedJobs> SavedJobs { get; set; }

        // Seed the Roles table with pre made roles data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { roleId = "R01", roleName = "Super Admin" },
                new Roles { roleId = "R02", roleName = "Admin" },
                new Roles { roleId = "R03", roleName = "User" }
            );
            modelBuilder.Entity<SavedJobs>()
            .HasOne(sj => sj.Job)
            .WithMany()
            .HasForeignKey(sj => sj.jobId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SavedJobs>()
                .HasOne(sj => sj.User)
                .WithMany()
                .HasForeignKey(sj => sj.userId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
