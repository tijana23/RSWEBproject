using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Feit.Models;

namespace Feit.Data
{
    public class FeitContext : DbContext
    {
        public FeitContext (DbContextOptions<FeitContext> options)
            : base(options)
        {
        }

        public DbSet<Feit.Models.Course> Course { get; set; }

        public DbSet<Feit.Models.Enrollment> Enrollment { get; set; }

        public DbSet<Feit.Models.Student> Student { get; set; }

        public DbSet<Feit.Models.Teacher> Teacher { get; set; }
    }
}
