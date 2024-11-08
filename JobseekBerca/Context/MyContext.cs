using Microsoft.EntityFrameworkCore;
using JobseekBerca.Models;

namespace JobseekBerca.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
    }
}
