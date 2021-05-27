using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Feit.Areas.Identity.Data;
using Feit.Models;

namespace Feit.Data
{
    public class FeitContext : IdentityDbContext<FeitUser>
    {
        public FeitContext(DbContextOptions<FeitContext> options): base(options)
        {

        }

        public DbSet<Feit.Models.Course> Course { get; set; }

        public DbSet<Feit.Models.Enrollment> Enrollment { get; set; }

        public DbSet<Feit.Models.Student> Student { get; set; }

        public DbSet<Feit.Models.Teacher> Teacher { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook");
            }
        }
    }
}
