using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FerumChecker.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region User
        ApplicationUserManager ApplicationUsers { get; }
        IRepository<UserProfile> UserProfiles { get; }
        ApplicationRoleManager Roles { get; }
        public IRepository<Comment> Comments { get; }
        public IRepository<ComputerAssemblyRate> ComputerAssemblyRates { get; }

        #endregion

        #region Infrastructure
        public IRepository<ComputerAssembly> ComputerAssemblies { get; }
        public IRepository<Manufacturer> Manufacturers { get; }
        public IRepository<Software> Softwares { get; }
        public IRepository<Developer> Developers { get; }
        public IRepository<Publisher> Publishers { get; }
        public IRepository<Country> Countries { get; }

        #endregion

        #region Specification

        public IRepository<CPUSocket> CPUSockets { get; }
        public IRepository<GPU> GPUs { get; }
        public IRepository<GraphicMemoryType> GraphicMemoryTypes { get; }
        public IRepository<MotherBoardFormFactor> MotherBoardFormFactors { get; }
        public IRepository<MotherBoardNorthBridge> MotherBoardNorthBridges { get; }
        public IRepository<OuterMemoryFormFactor> OuterMemoryFormFactors { get; }
        public IRepository<OuterMemoryInterface> OuterMemoryInterfaces { get; }
        public IRepository<PowerSupplyCPUInterface> PowerSupplyCPUInterfaces { get; }
        public IRepository<PowerSupplyMotherBoardInterface> PowerSupplyMotherBoardInterfaces { get; }
        public IRepository<RAMType> RAMTypes { get; }
        public IRepository<RequirementType> RequirementTypes { get; }
        public IRepository<VideoCardInterface> VideoCardInterfaces { get; }
        #endregion

        #region Joins
        public IRepository<ComputerAssemblyHDD> ComputerAssemblyHDDs { get; }
        public IRepository<ComputerAssemblyRAM> ComputerAssemblyRAMs { get; }
        public IRepository<ComputerAssemblySSD> ComputerAssemblySSDs { get; }
        public IRepository<ComputerAssemblyVideoCard> ComputerAssemblyVideoCards { get; }
        public IRepository<MotherBoardOuterMemorySlot> MotherBoardOuterMemorySlots { get; }
        public IRepository<MotherBoardRAMSlot> MotherBoardRAMSlots { get; }
        public IRepository<MotherBoardPowerSupplySlot> MotherBoardPowerSupplySlots { get; }
        public IRepository<MotherBoardVideoCardSlot> MotherBoardVideoCardSlots { get;}
        public IRepository<PCCaseMotherBoardFormFactor> PCCaseMotherBoardFormFactors { get; }
        public IRepository<PCCaseOuterMemoryFormFactor> PCCaseOuterMemoryFormFactors { get; }
        public IRepository<PowerSupplyPowerSupplyCPUInterface> PowerSupplyPowerSupplyCPUInterfaces { get; }
        public IRepository<SoftwareCPURequirement> SoftwareCPURequirements { get; }
        public IRepository<SoftwareVideoCardRequirement> SoftwareVideoCardRequirements { get; }
        #endregion

        #region Hardware
        public IRepository<CPU> CPUs { get; }
        public IRepository<HDD> HDDs { get; }
        public IRepository<MotherBoard> MotherBoards { get; }
        public IRepository<PCCase> PCCases { get; }
        public IRepository<PowerSupply> PowerSupplies { get; }
        public IRepository<RAM> RAMs { get; }
        public IRepository<SSD> SSDs { get; }
        public IRepository<VideoCard> VideoCards { get; }
        #endregion
        void Save();

        Task SaveAsync();
    }
}
