using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.DTO.Hardware;
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
            if (cpu == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }

            var result = true;
            var resultMessages = new List<string>();

            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                if (computerAssembly.MotherBoard.CPUSocketId != cpu.CPUSocketId)
                {
                    result = false;
                    resultMessages.Add("Сокети на процесорі (" + cpu.CPUSocket.Name + ") та материнській платі (" + computerAssembly.MotherBoard.CPUSocket.Name + ") не співпадають");
                }
            }
            if (result)
            {
                computerAssembly.CPUId = cpu.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            return new OperationDetails(result, resultMessages, "");
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
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.CPU != null)
            {
                computerAssembly.CPU = Database.CPUs.Get(computerAssembly.CPUId.Value);
                if (computerAssembly.CPU.CPUSocketId != motherBoard.CPUSocketId)
                {
                    result = false;
                    resultMessages.Add("Сокети на процесорі (" + computerAssembly.CPU.CPUSocket.Name + ") та материнській платі (" + motherBoard.CPUSocket.Name + ") не співпадають");
                }
            }
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                if (computerAssembly.PCCase.PCCaseMotherBoardFormFactors == null || !computerAssembly.PCCase.PCCaseMotherBoardFormFactors.Where(m => m.MotherBoardFormFactorId == motherBoardId).Any())
                {
                    result = false;
                    resultMessages.Add("Материнська плата має форм-фактор відсутній в обраному корпусі (форм-фактор материнської плати: " + motherBoard.MotherBoardFormFactor.Name + ")");
                }
            }
            var bufferList = new List<ComputerAssemblyRAM>(computerAssembly.ComputerAssemblyRAMs);
            foreach (var slot in motherBoard.MotherBoardRAMSlots)
            {
                var usedRAM = bufferList.FirstOrDefault(m => m.RAM.RAMTypeId == slot.RAMTypeId);
                if (usedRAM != null)
                {
                    bufferList.Remove(usedRAM);
                }
            }
            if (bufferList.Any())
            {
                result = false;
                resultMessages.Add("Материнська плата не має підходячих інтерфейсів для ОЗП");
            }
            var bufferList2 = new List<ComputerAssemblyHDD>(computerAssembly.HDDs);
            foreach (var slot in motherBoard.MotherBoardOuterMemorySlots)
            {
                var usedRAM = bufferList2.FirstOrDefault(m => m.HDD.OuterMemoryInterfaceId == slot.OuterMemoryInterfaceId);
                if (usedRAM != null)
                {
                    bufferList2.Remove(usedRAM);
                }
            }
            if (bufferList2.Any())
            {
                result = false;
                resultMessages.Add("Материнська плата не має підходячих інтерфейсів для HHD диска");
            }
            var bufferList3 = new List<ComputerAssemblySSD>(computerAssembly.SSDs);
            foreach (var slot in motherBoard.MotherBoardOuterMemorySlots)
            {
                var usedRAM = bufferList3.FirstOrDefault(m => m.SSD.OuterMemoryInterfaceId == slot.OuterMemoryInterfaceId);
                if (usedRAM != null)
                {
                    bufferList3.Remove(usedRAM);
                }
            }
            if (bufferList3.Any())
            {
                result = false;
                resultMessages.Add("Материнська плата не має підходячих інтерфейсів для SSD диска");
            }
            if (result)
            {
                computerAssembly.MotherBoardId = motherBoard.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            var details = new OperationDetails(result, resultMessages, "");
            details.RamFree = motherBoard.MotherBoardRAMSlots.Count();
            details.MemoryFree = motherBoard.MotherBoardOuterMemorySlots.Count();
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                details.MemoryFree = Math.Min(computerAssembly.PCCase.PCCaseOuterMemoryFormFactors.Count(), details.MemoryFree.Value);
            }
            return details;
        }

        public OperationDetails SetSSD(int ssdId, int assemblyId)
        {
            var ssd = Database.SSDs.Get(ssdId);
            if (ssd == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                var freeSlots = GetFreeOuterSlotsForMotherBoard(computerAssembly);
                if (!freeSlots.Any(m => m.OuterMemoryInterfaceId == ssd.OuterMemoryInterfaceId))
                {
                    result = false;
                    resultMessages.Add("Материнська плата не містить інтерфейсу для зовнішньої памяті   (" + ssd.OuterMemoryInterface.Name + ").");
                }
            }
            if (computerAssembly.PCCase != null)
            {
                var freeSlots = GetFreeOuterSlotsForPCCase(computerAssembly);
                if (!freeSlots.Any(m => m.OuterMemoryFormFactorId == ssd.OuterMemoryFormFactorId))
                {
                    result = false;
                    resultMessages.Add("Корпус не містить форм фактору для зовнішньої памяті  (" + ssd.OuterMemoryFormFactor.Name + ").");
                }
            }

            if (result)
            {
                var item = new ComputerAssemblySSD();
                item.ComputerAssemblyId = computerAssembly.Id;
                item.SSDId = ssd.Id;
                Database.ComputerAssemblySSDs.Create(item);
                Database.Save();
            }
            return new OperationDetails(result, resultMessages, "");
        }

        public OperationDetails SetHDD(int hddId, int assemblyId)
        {
            var hdd = Database.HDDs.Get(hddId);
            if (hdd == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                var freeSlots = GetFreeOuterSlotsForMotherBoard(computerAssembly);
                if (!freeSlots.Any(m => m.OuterMemoryInterfaceId == hdd.OuterMemoryInterfaceId))
                {
                    result = false;
                    resultMessages.Add("Материнська плата не містить інтерфейсу для зовнішньої памяті   (" + hdd.OuterMemoryInterface.Name + ").");
                }
            }
            if (computerAssembly.PCCase != null)
            {
                var freeSlots = GetFreeOuterSlotsForPCCase(computerAssembly);
                if (!freeSlots.Any(m => m.OuterMemoryFormFactorId == hdd.OuterMemoryFormFactorId))
                {
                    result = false;
                    resultMessages.Add("Корпус не містить форм фактору для зовнішньої памяті  (" + hdd.OuterMemoryFormFactor.Name + ").");
                }
            }

            if (result)
            {
                var item = new ComputerAssemblyHDD();
                item.ComputerAssemblyId = computerAssembly.Id;
                item.HDDId = hdd.Id;
                Database.ComputerAssemblyHDDs.Create(item);
                Database.Save();
            }
            return new OperationDetails(result, resultMessages, "");
        }

        public OperationDetails SetPCCase(int pcCaseId, int assemblyId)
        {
            var pcCase = Database.PCCases.Get(pcCaseId);
            if (pcCase == null)
            {
                return new OperationDetails(false, new List<string>() { "Апаратне забезпечення не знайдене" }, "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, new List<string>() { "Збірка не знайдена" }, "");
            }
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                if (!pcCase.PCCaseMotherBoardFormFactors.Where(m => m.MotherBoardFormFactorId == computerAssembly.MotherBoard.MotherBoardFormFactorId).Any())
                {
                    result = false;
                    resultMessages.Add("Даний корпус не має форм-фактора для обраної материнської плати (форм-фактор материнської плати: " + computerAssembly.MotherBoard.MotherBoardFormFactor.Name + ")");
                }
            }
            var bufferList2 = new List<ComputerAssemblyHDD>(computerAssembly.HDDs);
            foreach (var slot in pcCase.PCCaseOuterMemoryFormFactors)
            {
                var usedRAM = bufferList2.FirstOrDefault(m => m.HDD.OuterMemoryFormFactorId == slot.OuterMemoryFormFactorId);
                if (usedRAM != null)
                {
                    bufferList2.Remove(usedRAM);
                }
            }
            if (bufferList2.Any())
            {
                result = false;
                resultMessages.Add("Корпус не має підходячих форм-факторів для HHD диска");
            }
            var bufferList3 = new List<ComputerAssemblySSD>(computerAssembly.SSDs);
            foreach (var slot in pcCase.PCCaseOuterMemoryFormFactors)
            {
                var usedRAM = bufferList3.FirstOrDefault(m => m.SSD.OuterMemoryFormFactorId == slot.OuterMemoryFormFactorId);
                if (usedRAM != null)
                {
                    bufferList3.Remove(usedRAM);
                }
            }
            if (bufferList3.Any())
            {
                result = false;
                resultMessages.Add("Корпус не має підходячих форм-факторів для SSD диска");
            }
            if (result)
            {
                computerAssembly.PCCaseId = pcCase.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            var details = new OperationDetails(result, resultMessages, "");
            details.MemoryFree = pcCase.PCCaseOuterMemoryFormFactors.Count();
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                details.MemoryFree = Math.Min(computerAssembly.MotherBoard.MotherBoardOuterMemorySlots.Count(), details.MemoryFree.Value);
            }
            return details;
        }

        public OperationDetails SetPowerSupply(int powerSupplyId, int assemblyId)
        {
            var powerSupply = Database.PowerSupplies.Get(powerSupplyId);
            if (powerSupply == null)
            {
                return new OperationDetails(false, new List<string>() { "Апаратне забезпечення не знайдене" }, "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, new List<string>() { "Збірка не знайдена" }, "");
            }
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                if (!computerAssembly.MotherBoard.PowerSupplyMotherBoardSlots.Where(m => m.PowerSupplyMotherBoardInterfaceId == powerSupply.PowerSupplyMotherBoardInterfaceId).Any())
                {
                    result = false;
                    resultMessages.Add("Обрана материнська плата не має інтерфейсу для даного блоку живлення (" + powerSupply.PowerSupplyMotherBoardInterface.Name + ")");
                }
            }
            if (computerAssembly.VideoCards != null)
            {
                if (computerAssembly.VideoCards.Any(m => m.VideoCard.MinimumPowerConsuming > powerSupply.Power))
                {
                    result = false;
                    resultMessages.Add("Блока живлення не вистачить для коректної роботи обраної відеокарти (мінімум " + computerAssembly.VideoCards.ElementAt(0).VideoCard.MinimumPowerConsuming + " Вт )");
                }
            }
            if (result) {
                computerAssembly.PowerSupplyId = powerSupply.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            return new OperationDetails(result, resultMessages, "");
        }

        public OperationDetails SetVideoCard(int videoCardId, int assemblyId)
        {
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
            var result = true;
            var resultMessages = new List<string>();
            var warnings = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                var motherboard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                if (!motherboard.MotherBoardVideoCardSlots.Where(m => m.VideoCardInterface.Version >= videoCard.VideoCardInterface.Version).Any())
                {
                    result = false;
                    resultMessages.Add("Материнська плата не має інтерфейсу для цієї відео карти!");
                }
                if (!motherboard.MotherBoardVideoCardSlots.Where(m => m.VideoCardInterface.Multiplier >= videoCard.VideoCardInterface.Multiplier).Any())
                {
                    warnings.Add("Материнська плата не має оптимального прискорювача для інтерфейсу відео карти ( x " + videoCard.VideoCardInterface.Multiplier + "). Можливе використання графычного процесора не на повну потужність");
                }
            }
            if (result)
            {
                SetVideoCards(computerAssembly, new List<VideoCard> { videoCard });
                Database.Save();
            }
            return new OperationDetails(result, resultMessages, "", warnings);
        }

        public OperationDetails SetRAM(int ramId, int assemblyId)
        {
            var ram = Database.RAMs.Get(ramId);
            if (ram == null)
            {
                return new OperationDetails(false, "Апаратне забезпечення не знайдене", "");
            }
            var computerAssembly = Database.ComputerAssemblies.Get(assemblyId);
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                var freeSlots = GetFreeRamSlots(computerAssembly);
                if (!freeSlots.Any(m => m.RAMTypeId == ram.RAMTypeId))
                {
                    result = false;
                    resultMessages.Add("Материнська плата не містить входу для цієї оперативної памяті  (" + ram.RAMType.Name + ").");
                }
            }

            if (result)
            {
                var item = new ComputerAssemblyRAM();
                item.ComputerAssemblyId = computerAssembly.Id;
                item.RAMId = ramId;
                Database.ComputerAssemblyRAMs.Create(item);
                Database.Save();
            }
            return new OperationDetails(result, resultMessages, "");
        }


        public OperationDetails RemoveCPU(ComputerAssembly assembly)
        {
            if (assembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            assembly.CPUId = null;
            Database.ComputerAssemblies.Update(assembly);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }


        public OperationDetails RemoveVideoCard(ComputerAssembly assembly)
        {
            if (assembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            SetVideoCards(assembly, new List<VideoCard>());
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }


        public OperationDetails RemoveMotherBoard(ComputerAssembly assembly)
        {
            if (assembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            assembly.MotherBoardId = null;
            Database.ComputerAssemblies.Update(assembly);
            Database.Save();
            var result = new OperationDetails(true, "Ok", "");
            result.RamFree = 1;
            result.MemoryFree = 1;
            if (assembly.PCCase != null)
            {
                assembly.PCCase = Database.PCCases.Get(assembly.PCCaseId.Value);
                result.MemoryFree = assembly.PCCase.PCCaseOuterMemoryFormFactors.Count();
            }
            return result;

        }


        public OperationDetails RemovePCCase(ComputerAssembly assembly)
        {
            if (assembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            assembly.PCCaseId = null;
            Database.ComputerAssemblies.Update(assembly);
            Database.Save();
            var result = new OperationDetails(true, "Ok", "");
            result.RamFree = 1;
            result.MemoryFree = 1;
            if (assembly.MotherBoard != null)
            {
                assembly.MotherBoard = Database.MotherBoards.Get(assembly.MotherBoardId.Value);
                result.MemoryFree = assembly.MotherBoard.MotherBoardOuterMemorySlots.Count();
            }
            return result;
        }


        public OperationDetails RemovePowerSupply(ComputerAssembly assembly)
        {
            if (assembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            assembly.PowerSupplyId = null;
            Database.ComputerAssemblies.Update(assembly);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
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

        public OperationDetails RemoveSSD(ComputerAssembly computerAssembly, int id)
        {
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            var ssd = computerAssembly.SSDs.FirstOrDefault(m => m.SSDId == id);
            if (ssd != null)
            {
                Database.ComputerAssemblySSDs.Delete(ssd.Id);
                Database.Save();
            }
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails RemoveHDD(ComputerAssembly computerAssembly, int id)
        {
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            var hdd = computerAssembly.HDDs.FirstOrDefault(m => m.HDDId == id);
            if (hdd != null)
            {
                Database.ComputerAssemblyHDDs.Delete(hdd.Id);
                Database.Save();
            }
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails RemoveRam(ComputerAssembly computerAssembly, int id)
        {
            if (computerAssembly == null)
            {
                return new OperationDetails(false, "Збірка не знайдена", "");
            }
            var ram = computerAssembly.ComputerAssemblyRAMs.FirstOrDefault(m => m.RAMId == id);
            if (ram != null)
            {
                Database.ComputerAssemblyRAMs.Delete(ram.Id);
                Database.Save();
            }
            return new OperationDetails(true, "Ok", "");
        }

        public int CalculateFreeRAMSlot(ComputerAssembly computerAssembly)
        {
            if (computerAssembly == null)
            {
                return 0;
            }
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                var result = computerAssembly.MotherBoard.MotherBoardRAMSlots.Count() - computerAssembly.ComputerAssemblyRAMs.Count();
                return result;
            }
            return computerAssembly.ComputerAssemblyRAMs.Count() > 0 ? 0 : 1;
        }

        public int CalculateFreeOuterMemorySlot(ComputerAssembly computerAssembly)
        {
            if (computerAssembly == null)
            {
                return 0;
            }
            var motherBoardSlots = -1;
            var pcCaseSlots = -1;
            var freeSlots = 0;
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                motherBoardSlots = computerAssembly.MotherBoard.MotherBoardOuterMemorySlots.Count();
            }
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                pcCaseSlots = computerAssembly.PCCase.PCCaseOuterMemoryFormFactors.Count();
            }
            if (pcCaseSlots == -1 && motherBoardSlots >= 0)
            {
                freeSlots = motherBoardSlots - (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count());
            }
            else if (pcCaseSlots >= 0 && motherBoardSlots == -1)
            {
                freeSlots = pcCaseSlots - (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count());
            }
            else if (pcCaseSlots > 0 && motherBoardSlots > 0)
            {
                freeSlots = Math.Min(pcCaseSlots, motherBoardSlots) - (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count());
            }
            else
            {
                freeSlots = (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count()) > 0 ? 0 : 1;
            }

            return freeSlots;
        }

        private IEnumerable<MotherBoardRAMSlot> GetFreeRamSlots(ComputerAssembly computerAssembly)
        {
            var result = new List<MotherBoardRAMSlot>();
            var bufferList = new List<ComputerAssemblyRAM>(computerAssembly.ComputerAssemblyRAMs);
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                foreach (var slot in computerAssembly.MotherBoard.MotherBoardRAMSlots)
                {
                    var usedRAM = bufferList.FirstOrDefault(m => m.RAM.RAMTypeId == slot.RAMTypeId);
                    if (usedRAM == null)
                    {
                        result.Add(slot);
                    }
                    else
                    {
                        bufferList.Remove(usedRAM);
                    }
                }

            }
            return result;
        }


        private IEnumerable<MotherBoardOuterMemorySlot> GetFreeOuterSlotsForMotherBoard(ComputerAssembly computerAssembly)
        {
            List<MotherBoardOuterMemorySlot> result1 = new List<MotherBoardOuterMemorySlot>();
            List<MotherBoardOuterMemorySlot> result2 = new List<MotherBoardOuterMemorySlot>();
            var bufferSSD = new List<ComputerAssemblySSD>(computerAssembly.SSDs);
            var bufferHDD = new List<ComputerAssemblyHDD>(computerAssembly.HDDs);
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                foreach (var slot in computerAssembly.MotherBoard.MotherBoardOuterMemorySlots)
                {
                    var usedSSD = bufferSSD.FirstOrDefault(m => m.SSD.OuterMemoryInterfaceId == slot.OuterMemoryInterfaceId);
                    if (usedSSD == null)
                    {
                        result1.Add(slot);
                    }
                    else
                    {
                        bufferSSD.Remove(usedSSD);
                    }
                }
                foreach (var slot in result1)
                {
                    var usedHDD = bufferHDD.FirstOrDefault(m => m.HDD.OuterMemoryInterfaceId == slot.OuterMemoryInterfaceId);
                    if (usedHDD == null)
                    {
                        result2.Add(slot);
                    }
                    else
                    {
                        bufferHDD.Remove(usedHDD);
                    }
                }


            }
            return result2;
        }


        private IEnumerable<PCCaseOuterMemoryFormFactor> GetFreeOuterSlotsForPCCase(ComputerAssembly computerAssembly)
        {
            List<PCCaseOuterMemoryFormFactor> result1 = new List<PCCaseOuterMemoryFormFactor>();
            List<PCCaseOuterMemoryFormFactor> result2 = new List<PCCaseOuterMemoryFormFactor>();
            var bufferSSD = new List<ComputerAssemblySSD>(computerAssembly.SSDs);
            var bufferHDD = new List<ComputerAssemblyHDD>(computerAssembly.HDDs);
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                foreach (var slot in computerAssembly.PCCase.PCCaseOuterMemoryFormFactors)
                {
                    var usedSSD = bufferSSD.FirstOrDefault(m => m.SSD.OuterMemoryFormFactorId == slot.OuterMemoryFormFactorId);
                    if (usedSSD == null)
                    {
                        result1.Add(slot);
                    }
                    else
                    {
                        bufferSSD.Remove(usedSSD);
                    }
                }
                foreach (var slot in result1)
                {
                    var usedHDD = bufferHDD.FirstOrDefault(m => m.HDD.OuterMemoryFormFactorId == slot.OuterMemoryFormFactorId);
                    if (usedHDD == null)
                    {
                        result2.Add(slot);
                    }
                    else
                    {
                        bufferHDD.Remove(usedHDD);
                    }
                }


            }
            return result2;
        }

        public IEnumerable<RecomendationDTO> CreateRecomendations(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            result = result.Concat(CreateCPURecomendation(computerAssembly)).ToList();
            result = result.Concat(CreateMotherBoardRecomendation(computerAssembly)).ToList();
            result = result.Concat(CreateHDDRecomendation(computerAssembly)).ToList();
            result = result.Concat(CreatePCCaseRecomendation(computerAssembly)).ToList();
            result = result.Concat(CreateSSDRecomendation(computerAssembly)).ToList();
            result = result.Concat(CreateVideCardRecomendation(computerAssembly)).ToList();
            result = result.Concat(CreatePCCaseRecomendation(computerAssembly)).ToList();
            result = result.Concat(CreatePowerSupplyRecomendation(computerAssembly)).ToList();

            return result;
        }

        private IEnumerable<RecomendationDTO> CreateCPURecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            var type = "CPU";
            if (computerAssembly.MotherBoard != null && computerAssembly.CPU == null)
            {
                var cpus = Database.CPUs.GetAll().Where(m => m.CPUSocketId == computerAssembly.MotherBoard.CPUSocketId).Take(2);
                result = cpus.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "Процесор",
                    Name = m.Name,
                    ImagePath = "/Images/CPU/" + m.Image,
                    Type = type
                }).ToList();
            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateMotherBoardRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateSSDRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateRAMRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateHDDRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateVideCardRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }

        private IEnumerable<RecomendationDTO> CreatePowerSupplyRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }

        private IEnumerable<RecomendationDTO> CreatePCCaseRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            return result;
        }


       private bool IsFull(int id)
        {
            var assenbly = Database.ComputerAssemblies.Get(id);
            if(assenbly == null)
            {
                return false;
            }
            return assenbly.CPU != null && assenbly.MotherBoard != null && assenbly.PowerSupply != null
                && assenbly.PCCase != null && assenbly.VideoCards.Count() > 0
                && assenbly.ComputerAssemblyRAMs.Count() > 0 && (assenbly.HDDs.Count() + assenbly.SSDs.Count() > 0);
        }
    }
}
