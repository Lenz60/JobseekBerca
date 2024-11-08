using Microsoft.EntityFrameworkCore;
using JobseekBerca.Models;

namespace JobseekBerca.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Applications> Applications { get; set; }


    }
}
