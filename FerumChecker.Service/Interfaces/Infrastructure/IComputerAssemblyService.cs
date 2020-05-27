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

        OperationDetails OnRAMDelete(int id);
        OperationDetails OnCPUDelete(int id);
        OperationDetails OnMotherBoardDelete(int id);
        OperationDetails OnSSDDelete(int id);
        OperationDetails OnHDDDelete(int id);
        OperationDetails OnPCCaseDelete(int id);
        OperationDetails OnPowerSupplyDelete(int id);
        OperationDetails OnVideoCardDelete(int id);
        OperationDetails OnRAMChange(int id);
        OperationDetails OnCPUChange(int id);
        OperationDetails OnMotherBoardChange(int id);
        OperationDetails OnSSDChange(int id);
        OperationDetails OnHDDChange(int id);
        OperationDetails OnPCCaseChange(int id);
        OperationDetails OnPowerSupplyChange(int id);
        OperationDetails OnVideoCardChange(int id);

        OperationDetails SoftwareSyncEvaluate(int id, ComputerAssembly computerAssembly);

        int GetTotalRAM(ComputerAssembly computerAssembly);

        int GetTotalVolume(ComputerAssembly computerAssembly);
        public int CalculateFreeRAMSlot(ComputerAssembly computerAssembly);

        public int CalculateFreeOuterMemorySlot(ComputerAssembly computerAssembly);

        public IEnumerable<RecomendationDTO> CreateRecomendations(ComputerAssembly computerAssembly);
        void Dispose();
    }
}
