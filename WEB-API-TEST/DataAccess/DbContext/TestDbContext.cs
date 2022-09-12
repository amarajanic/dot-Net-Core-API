using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbModel;
using DataAccess.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContext
{
    public class TestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {

        }

        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbRole> Roles { get; set; }
        public DbSet<DbTask> Tasks { get; set; }
        public DbSet<DbPerk> Perks { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=TestDB;Trusted_Connection=True;");
        }
    }
}
