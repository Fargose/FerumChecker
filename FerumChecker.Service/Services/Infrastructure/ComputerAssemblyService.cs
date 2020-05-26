using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces;
using FerumChecker.Service.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Service.Services.Infrastructure
{
    public class ComputerAssemblyService : IComputerAssemblyService
    {
        IUnitOfWork Database { get; set; }

        public ComputerAssemblyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ComputerAssembly> GetComputerAssemblies()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            return Database.ComputerAssemblies.GetAll();
        }

        public ComputerAssembly GetComputerAssembly(int? id)
        {
            var computerAssembly = Database.ComputerAssemblies.Get(id.Value);

            return computerAssembly;
        }


        public OperationDetails UpdateComputerAssembly(ComputerAssembly computerAssembly)
        {

            Database.ComputerAssemblies.Update(computerAssembly);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateComputerAssembly(ComputerAssembly computerAssembly)
        {
            Database.ComputerAssemblies.Create(computerAssembly);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteComputerAssembly(int? id)
        {
            Database.ComputerAssemblies.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public OperationDetails SetCPU(int cpuId, int assemblyId)
        {
            var cpu = Database.CPUs.Get(cpuId);
            if(cpu == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }

            if(computerAssembly.MotherBoard != null)
            {
                if(computerAssembly.MotherBoard.CPUSocketId != cpu.CPUSocketId)
                {
                    return new OperationDetails(false, "Сокети на процесорі та материнській платі не співпадають", "");
                }
            }
            computerAssembly.CPUId = cpu.Id;
            UpdateComputerAssembly(computerAssembly);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails SetMotherBoard(int motherBoardId, int assemblyId)
        {
            var motherBoard = Database.MotherBoards.Get(motherBoardId);
            if (motherBoard == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }

            if (computerAssembly.CPU != null)
            {
                if (computerAssembly.CPU.CPUSocketId != motherBoard.CPUSocketId)
                {
                    return new OperationDetails(false, "Сокети на процесорі та материнській платі не співпадають", "");
                }
            }
            computerAssembly.MotherBoardId = motherBoard.Id;
            UpdateComputerAssembly(computerAssembly);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails SetSSD(int ssdId, int assemblyId)
        {
            throw new NotImplementedException();
        }

        public OperationDetails SetHDD(int hddId, int assemblyId)
        {
            throw new NotImplementedException();
        }

        public OperationDetails SetPCCase(int pcCaseId, int assemblyId)
        {
            throw new NotImplementedException();
        }

        public OperationDetails SetPowerSupply(int powerSupplyId, int assemblyId)
        {
            throw new NotImplementedException();
        }

        public OperationDetails SetVideoCard(int videoCardId, int assemblyId)
        {
            string warning = null;
            var videoCard = Database.VideoCards.Get(videoCardId);
            if (videoCard == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }

            if (computerAssembly.MotherBoard != null)
            {
                var motherboard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                if (!motherboard.MotherBoardVideoCardSlots.Where(m => m.VideoCardInterface.Version >= videoCard.VideoCardInterface.Version).Any())
                {
                    return new OperationDetails(false, "Материнська плата не має інтерфейсу для цієї відео карти!", "");
                }
                if(!motherboard.MotherBoardVideoCardSlots.Where(m => m.VideoCardInterface.Multiplier >= videoCard.VideoCardInterface.Multiplier).Any())
                {
                    warning = "Материнська плата не має оптимального прискорювача для інтерфейсу відео карти ( x " +videoCard.VideoCardInterface.Multiplier + "). Можливе використання графычного процесора не на повну потужність";
                }
            }
            SetVideoCards(computerAssembly, new List<VideoCard> { videoCard });
            Database.Save();
            return new OperationDetails(true, "Ok", "", warning);
        }

        public OperationDetails SetRAM(int ramId, int assemblyId)
        {
            throw new NotImplementedException();
        }

        private OperationDetails SetVideoCards(ComputerAssembly computerAssembly, List<VideoCard> videoCards)
        {

            if (computerAssembly.Id > -1 && videoCards != null)
            {
                var oldOuterMemory = Database.ComputerAssemblyVideoCards.GetAll().Where(m => m.ComputerAssemblyId == computerAssembly.Id);
                foreach (var item in oldOuterMemory)
                {
                    Database.ComputerAssemblyVideoCards.Delete(item.Id);
                }
                foreach (var item in videoCards)
                {
                    var entry = new ComputerAssemblyVideoCard();
                    entry.ComputerAssemblyId = computerAssembly.Id;
                    entry.VideoCardId = item.Id;
                    this.Database.ComputerAssemblyVideoCards.Create(entry);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }
    }
}
