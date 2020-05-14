using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.DataAccess.Map;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.EF
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<ComputerAssembly> ComputerAssemblies { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CPUSocket> CPUSockets { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GraphicMemoryType> GraphicMemoryTypes { get; set; }
        public DbSet<VideoCardInterface> VideoCardInterfaces { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<VideoCard> VideoCards { get; set; }
        public DbSet<RAMType> RAMTypes { get; set; }
        public DbSet<RAM> RAMs { get; set; }
        public DbSet<OuterMemoryFormFactor> OuterMemoryFormFactors { get; set; }
        public DbSet<OuterMemoryInterface> OuterMemoryInterfaces { get; set; }
        public DbSet<HDD> HDDs { get; set; }
        public DbSet<SSD> SSDs { get; set; }
        public DbSet<PowerSupply> PowerSupplies { get; set; }
        public DbSet<PowerSupplyCPUInterface> PowerSupplyCPUInterfaces { get; set; }
        public DbSet<PowerSupplyMotherBoardInterface> PowerSupplyMotherBoardInterfaces { get; set; }
        public DbSet<MotherBoardFormFactor> MotherBoardFormFactors { get; set; }
        public DbSet<MotherBoardNothernBridge> MotherBoardNothernBridges { get; set; }
        public DbSet<MotherBoardOuterMemorySlot> MotherBoardOuterMemorySlots { get; set; }
        public DbSet<MotherBoardRAMSlot> MotherBoardRAMSlots { get; set; }
        public DbSet<MotherBoardPowerSupplySlot> MotherBoardPowerSupplySlots { get; set; }
        public DbSet<MotherBoardVideoCardSlot> MotherBoardVideoCardSlots { get; set; }
        public DbSet<MotherBoard> MotherBoards { get; set; }
        public DbSet<PCCaseMotherBoardFormFactor> PCCaseMotherBoardFormFactors { get; set; }
        public DbSet<PCCaseOuterMemoryFormFactor> PCCaseOuterMemoryFormFactors { get; set; }
        public DbSet<PCCase> PCCases { get; set; }
        public DbSet<ComputerAssemblyHDD> ComputerAssemblyHDDs { get; set; }
        public DbSet<ComputerAssemblySSD> ComputerAssemblySSDs { get; set; }
        public DbSet<ComputerAssemblyRAM> ComputerAssemblyRAMs { get; set; }
        public DbSet<ComputerAssemblyVideoCard> ComputerAssemblyVideoCards { get; set; }
        public DbSet<ComputerAssemblyRate> ComputerAssemblyRates { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PowerSupplyPowerSupplyCPUInterface> PowerSupplyPowerSupplyCPUInterfaces { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<RequirementType> RequirementTypes { get; set; }
        public DbSet<SoftwareCPURequirement> SoftwareCPURequirements { get; set; }
        public DbSet<SoftwareVideoCardRequirement> SoftwareVideoCardRequirements { get; set; }
        public DbSet<Software> Softwares { get; set; }



        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PowerSupplyPowerSupplyCPUInterface>()
                .HasKey(t => new { t.PowerSupplyId, t.PowerSupplyCPUInterfaceId });
            modelBuilder.Entity<ComputerAssembly>()
             .HasOne(t => t.PCCase);
            modelBuilder.Entity<ComputerAssembly>()
       .HasOne(t => t.PowerSupply);
            modelBuilder.Entity<ComputerAssembly>()
       .HasOne(t => t.MotherBoard);
            modelBuilder.Entity<ComputerAssembly>()
       .HasOne(t => t.CPU);
            // new ComputerAssemblyMap(modelBuilder.Entity<ComputerAssembly>());
        }
    }
}
