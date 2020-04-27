using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.EF
{
    public class ApplicationContext: DbContext
    {
        public DbSet<ComputerAssembly> ComputerAssemblies { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ComputerAssemblyMap(modelBuilder.Entity<ComputerAssembly>());
        }
    }
}
