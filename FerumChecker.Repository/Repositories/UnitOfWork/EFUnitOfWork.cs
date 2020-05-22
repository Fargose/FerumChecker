using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Identity;
using FerumChecker.Repository.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FerumChecker.Repository.Repositories.Infrastructure;
using FerumChecker.Repository.Repositories.User;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.Repositories.Specification;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.Repository.Repositories.Joins;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.Repository.Repositories.Hardware;

namespace FerumChecker.Repository.Repositories.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;


        public EFUnitOfWork(ApplicationContext context)
        {
            db = context;
        }

        public EFUnitOfWork(ApplicationContext db, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> optionAccessor, Microsoft.AspNetCore.Identity.IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<Microsoft.AspNetCore.Identity.IUserValidator<ApplicationUser>> userValidators, IEnumerable<Microsoft.AspNetCore.Identity.IPasswordValidator<ApplicationUser>> passwordValidators, Microsoft.AspNetCore.Identity.ILookupNormalizer keyNormalizer, Microsoft.AspNetCore.Identity.IdentityErrorDescriber errors, IServiceProvider servises, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>> logger, IEnumerable<Microsoft.AspNetCore.Identity.IRoleValidator<ApplicationRole>> roleValidators, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>> roleLogger)
        {
            this.db = db;

            #region User repositories

            ApplicationUsers = new ApplicationUserManager(new UserStore<ApplicationUser>(db), optionAccessor, passwordHasher, userValidators, passwordValidators,keyNormalizer, errors, servises, logger);
            Roles = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db), roleValidators, keyNormalizer, errors, roleLogger);
            UserProfiles = new UserProfileRepository(db);
            Comments = new CommentRepository(db);
            ComputerAssemblyRates = new ComputerAssemblyRateRepository(db);

            #endregion

            #region Infrastructure repositories

            ComputerAssemblies = new ComputerAssemblyRepository(db);
            Manufacturers = new ManufacturerRepository(db);
            Softwares = new SoftwareRepository(db);
            Developers = new DeveloperRepository(db);
            Publishers = new PublisherRepository(db);
            Countries = new CountryRepository(db);

            #endregion

            #region Specification repositories

            CPUSockets = new CPUSocketRepository(db);
            GPUs = new GPURepository(db);
            GraphicMemoryTypes = new GraphicMemoryTypeRepository(db);
            MotherBoardFormFactors = new MotherBoardFormFactorRepository(db);
            MotherBoardNorthBridges = new MotherBoardNorthBridgeRepository(db);
            OuterMemoryFormFactors = new OuterMemoryFormFactorRepository(db);
            OuterMemoryInterfaces = new OuterMemoryInterfaceRepository(db);
            PowerSupplyCPUInterfaces = new PowerSupplyCPUInterfaceRepository(db);
            PowerSupplyMotherBoardInterfaces = new PowerSupplyMotherBoardInterfaceRepository(db);
            RAMTypes = new RAMTypeRepository(db);
            RequirementTypes = new RequirementTypeRepository(db);
            VideoCardInterfaces = new VideoCardInterfaceRepository(db);
            #endregion

            #region Joins repositories

            ComputerAssemblyHDDs = new ComputerAssemblyHDDRepository(db);
            ComputerAssemblyRAMs = new ComputerAssemblyRAMRepository(db);
            ComputerAssemblySSDs = new ComputerAssemblySSDRepository(db);
            ComputerAssemblyVideoCards = new ComputerAssemblyVideoCardRepository(db);
            MotherBoardOuterMemorySlots = new MotherBoardOuterMemorySlotRepository(db);
            MotherBoardRAMSlots = new MotherBoardRAMSlotRepository(db);
            MotherBoardPowerSupplySlots = new MotherBoardPowerSupplySlotRepository(db);
            MotherBoardVideoCardSlots = new MotherBoardVideoCardSlotRepository(db);
            PCCaseMotherBoardFormFactors = new PCCaseMotherBoardFormFactorRepository(db);
            PCCaseOuterMemoryFormFactors = new PCCaseOuterMemoryFormFactorRepository(db);
            PowerSupplyPowerSupplyCPUInterfaces = new PowerSupplyPowerSupplyCPUInterfaceRepository(db);
            SoftwareCPURequirements = new SoftwareCPURequirementRepository(db);
            SoftwareVideoCardRequirements = new SoftwareVideoCardRequirementRepository(db);
            #endregion

            #region Hardware Repositories 

            CPUs = new CPURepository(db);
            HDDs = new HDDRepository(db);
            MotherBoards = new MotherBoardRepository(db);
            PCCases = new PCCaseRepository(db);
            PowerSupplies = new PowerSupplyRepository(db);
            RAMs = new RAMRepository(db);
            SSDs = new SSDRepository(db);
            VideoCards = new VideoCardRepository(db);

            #endregion
        }

        #region User
        public ApplicationUserManager ApplicationUsers { get; }
        public IRepository<UserProfile> UserProfiles { get; }
        public ApplicationRoleManager Roles { get; }
        public IRepository<Comment> Comments { get; }
        public IRepository<ComputerAssemblyRate> ComputerAssemblyRates { get; }

        #endregion

        #region Infrastructure
        public IRepository<ComputerAssembly> ComputerAssemblies { get; }
        public IRepository<Manufacturer> Manufacturers { get; }
        public IRepository<Software> Softwares { get; } 
        public IRepository <Developer> Developers { get; }
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
        public IRepository<MotherBoardVideoCardSlot> MotherBoardVideoCardSlots { get; }
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
        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

