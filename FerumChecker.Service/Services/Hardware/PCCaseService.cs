using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using FerumChecker.Service.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class PCCaseService : IPCCaseService
    {

        IUnitOfWork Database { get; set; }

        IComputerAssemblyService _computerAssemblyService { get; set; }
        public PCCaseService(IUnitOfWork uow, IComputerAssemblyService computerAssemblyService)
        {
            Database = uow;
            _computerAssemblyService = computerAssemblyService;
        }

        public PCCase GetPCCase(int? id)
        {
            return Database.PCCases.Get(id.Value);
        }

        public IEnumerable<PCCase> GetPCCases()
        {
            return Database.PCCases.GetAll();
        }

        public OperationDetails UpdatePCCase(PCCase pcCase)
        {
            
            Database.PCCases.Update(pcCase);

            Database.Save();
            SetMotherBoardFormFactors(pcCase, (List<PCCaseMotherBoardFormFactor>)pcCase.PCCaseMotherBoardFormFactors);
            SetOuterMemoryFormFactors(pcCase, (List<PCCaseOuterMemoryFormFactor>)pcCase.PCCaseOuterMemoryFormFactors);
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreatePCCase(PCCase pcCase)
        {
            Database.PCCases.Create(pcCase);
            SetMotherBoardFormFactors(pcCase, (List<PCCaseMotherBoardFormFactor>)pcCase.PCCaseMotherBoardFormFactors);
            SetOuterMemoryFormFactors(pcCase, (List<PCCaseOuterMemoryFormFactor>)pcCase.PCCaseOuterMemoryFormFactors);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeletePCCase(int? id)
        {
            Database.PCCases.Delete(id.Value);
            Database.Save();

            return new OperationDetails(true, "Ok", "");
        }

        private OperationDetails SetOuterMemoryFormFactors(PCCase pcCase, List<PCCaseOuterMemoryFormFactor> outerMemories)
        {

            if (pcCase.Id > -1 && outerMemories != null)
            {
                var oldOuterMemory = Database.PCCaseOuterMemoryFormFactors.GetAll().Where(m => m.PCCaseId == pcCase.Id);
                foreach (var item in oldOuterMemory)
                {
                    Database.PCCaseOuterMemoryFormFactors.Delete(item.Id);
                }
                foreach (var item in outerMemories)
                {
                    item.PCCaseId = pcCase.Id;
                    this.Database.PCCaseOuterMemoryFormFactors.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }


        private OperationDetails SetMotherBoardFormFactors(PCCase pcCase, List<PCCaseMotherBoardFormFactor> outerMemories)
        {

            if (pcCase.Id > -1 && outerMemories != null)
            {
                var oldMotherBoardFormFactors = Database.PCCaseMotherBoardFormFactors.GetAll().Where(m => m.PCCaseId == pcCase.Id);
                foreach (var item in oldMotherBoardFormFactors)
                {
                    Database.PCCaseMotherBoardFormFactors.Delete(item.Id);
                }
                foreach (var item in outerMemories)
                {
                    item.PCCaseId = pcCase.Id;
                    this.Database.PCCaseMotherBoardFormFactors.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
