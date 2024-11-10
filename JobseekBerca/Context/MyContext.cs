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

        // Seed the Roles table with pre made roles data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { roleId = "R01", roleName = "Super Admin" },
                new Roles { roleId = "R02", roleName = "Admin" },
                new Roles { roleId = "R03", roleName = "User" }
            );
        }
    }
}
