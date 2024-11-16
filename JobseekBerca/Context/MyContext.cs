using Microsoft.EntityFrameworkCore;
using JobseekBerca.Models;
using System.Data;
using JobseekBerca.Helper;
using static JobseekBerca.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;

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
        public DbSet<UsersGoogle> UsersGoogle { get; set; }

        // Seed the Roles table with pre made roles data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            // Configure cascade delete for Roles
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany()
                .HasForeignKey(u => u.roleId)
                .OnDelete(DeleteBehavior.Cascade);
            // Seed roles
            modelBuilder.Entity<Roles>().HasData(
                new Roles { roleId = "R01", roleName = "Super Admin" },
                new Roles { roleId = "R02", roleName = "Admin" },
                new Roles { roleId = "R03", roleName = "User" }
            );

            // Seed the users
            var password = HashingHelper.HashPassword("123");
            var superAdmin = new Users
            {
                userId = ULIDHelper.GenerateULID(),
                email = "super@email.com",
                password = password,
                roleId = "R01"
            };
            var superProfile = new Profiles
            {
                userId = superAdmin.userId,
                fullName = "Super Admin",
                summary = $"Hello I'm a Super Admin",
                gender = null,
                address = null,
                birthDate = null,
                profileImage = "https://i1.wp.com/www.bulletproofaction.com/wp-content/uploads/2015/09/john-wick.jpg"
            };
            var admin = new Users
            {
                userId = ULIDHelper.GenerateULID(),
                email = "admin@email.com",
                password = password,
                roleId = "R02"
            };
            var adminProfile = new Profiles
            {
                userId = admin.userId,
                fullName = "Super Admin",
                summary = $"Hello I'm a Admin",
                gender = null,
                address = null,
                birthDate = null,
                profileImage = "https://www.looper.com/img/gallery/john-wick-chapter-4-caine-is-a-refreshing-take-an-old-action-trope/l-intro-1679619131.jpg"
            };
            modelBuilder.Entity<Users>().HasData(superAdmin,admin);
            modelBuilder.Entity<Profiles>().HasData(superProfile, adminProfile);

            
            // Cascade delete on the SavedJobs
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
