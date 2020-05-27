using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.Service.DTO.Hardware;
using FerumChecker.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Interfaces.Infrastructure
{
    public interface IComputerAssemblyService
    {
        ComputerAssembly GetComputerAssembly(int? id);
        IEnumerable<ComputerAssembly> GetComputerAssemblies();

        OperationDetails CreateComputerAssembly(ComputerAssembly computerAssembly);

        OperationDetails UpdateComputerAssembly(ComputerAssembly computerAssembly);

        OperationDetails DeleteComputerAssembly(int? id);

        OperationDetails SetCPU(int cpuId, int assemblyId);

        OperationDetails SetMotherBoard(int motherBoardId, int assemblyId);

        OperationDetails SetSSD(int ssdId, int assemblyId);

        OperationDetails SetHDD(int hddId, int assemblyId);

        OperationDetails SetPCCase(int pcCaseId, int assemblyId);

        OperationDetails SetPowerSupply(int powerSupplyId, int assemblyId);

        OperationDetails SetVideoCard(int videoCardId, int assemblyId);

        OperationDetails SetRAM(int ramId, int assemblyId);


        OperationDetails RemoveCPU(ComputerAssembly computerAssembly);

        OperationDetails RemoveMotherBoard(ComputerAssembly computerAssembly);

        OperationDetails RemoveSSD(ComputerAssembly computerAssembly, int id);

        OperationDetails RemoveHDD(ComputerAssembly computerAssembly, int id);

        OperationDetails RemovePCCase(ComputerAssembly computerAssembly);

        OperationDetails RemovePowerSupply(ComputerAssembly computerAssembly);

        OperationDetails RemoveVideoCard(ComputerAssembly computerAssembly);

        OperationDetails RemoveRam(ComputerAssembly computerAssembly, int id);

        public int CalculateFreeRAMSlot(ComputerAssembly computerAssembly);

        public int CalculateFreeOuterMemorySlot(ComputerAssembly computerAssembly);

        public IEnumerable<RecomendationDTO> CreateRecomendations(ComputerAssembly computerAssembly);
        void Dispose();
    }
}
