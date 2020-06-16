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

            var result = CheckCPU(cpu, computerAssembly);
            if (result.Succedeed)
            {
                computerAssembly.CPUId = cpu.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            return result;
        }

        private OperationDetails CheckCPU(CPU cpu, ComputerAssembly computerAssembly)
        {
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                if (computerAssembly.MotherBoard.CPUSocketId != cpu.CPUSocketId)
                {
                    result = false;
                    if (cpu.CPUSocket != null)
                    {
                        resultMessages.Add("Сокети на процесорі (" + cpu.CPUSocket.Name + ") та материнській платі (" + computerAssembly.MotherBoard.CPUSocket.Name + ") не співпадають");
                    }
                }
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
            var result = CheckMotherBoard(motherBoard, computerAssembly);
            if (result.Succedeed)
            {
                computerAssembly.MotherBoardId = motherBoard.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            var details = result;
            details.RamFree = motherBoard.MotherBoardRAMSlots.Count();
            details.MemoryFree = motherBoard.MotherBoardOuterMemorySlots.Count();
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                details.MemoryFree = Math.Min(computerAssembly.PCCase.PCCaseOuterMemoryFormFactors.Count(), details.MemoryFree.Value);
            }
            return details;
        }

        private OperationDetails CheckMotherBoard(MotherBoard motherBoard, ComputerAssembly computerAssembly)
        {
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.CPU != null)
            {
                computerAssembly.CPU = Database.CPUs.Get(computerAssembly.CPUId.Value);
                if (computerAssembly.CPU.CPUSocketId != motherBoard.CPUSocketId)
                {
                    result = false;
                    //if (motherBoard.CPUSocket != null)
                    {
                        resultMessages.Add("Сокети на процесорі (" + computerAssembly.CPU.CPUSocket.Name + ") та материнській платі (" + motherBoard.CPUSocket.Name + ") не співпадають");
                    }
                }
            }
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                if (computerAssembly.PCCase.PCCaseMotherBoardFormFactors == null || !computerAssembly.PCCase.PCCaseMotherBoardFormFactors.Where(m => m.MotherBoardFormFactorId == motherBoard.MotherBoardFormFactorId).Any())
                {
                    result = false;
                    resultMessages.Add("Материнська плата має форм-фактор відсутній в обраному корпусі (форм-фактор материнської плати: " + motherBoard.MotherBoardFormFactor.Name + ")");
                }
            }
            if(computerAssembly.VideoCards.Count() > 0)
            {
                var videoCard = Database.VideoCards.Get(computerAssembly.VideoCards.ElementAt(0).VideoCardId);
                if (!motherBoard.MotherBoardVideoCardSlots.Where(m => m.VideoCardInterface.Version >= videoCard.VideoCardInterface.Version).Any())
                {
                    result = false;
                    resultMessages.Add("Материнська плата не має інтерфейсу для встановленої відео карти!");
                }
            }
            var bufferResult = CheckMotherBoardOnRAM(motherBoard, computerAssembly);
            result = result && bufferResult.Succedeed;
            resultMessages = resultMessages.Concat(bufferResult.Messages).ToList();
            bufferResult = CheckMotherBoardOnOuterMemory(motherBoard, computerAssembly);
            result = result && bufferResult.Succedeed;
            resultMessages = resultMessages.Concat(bufferResult.Messages).ToList();
            return new OperationDetails(result, resultMessages, ""); ;
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

            var result = CheckSSD(ssd, computerAssembly);
            if (result.Succedeed)
            {
                var item = new ComputerAssemblySSD();
                item.ComputerAssemblyId = computerAssembly.Id;
                item.SSDId = ssd.Id;
                Database.ComputerAssemblySSDs.Create(item);
                Database.Save();
            }
            return result;
        }
        private OperationDetails CheckSSD(SSD ssd, ComputerAssembly computerAssembly)
        {
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
            if (computerAssembly.PowerSupply != null)
            {
                if (computerAssembly.PowerSupply.SATAInputNumber < computerAssembly.HDDs.Count() + computerAssembly.SSDs.Count() + 1)
                {
                    result = false;
                    resultMessages.Add("Блок не має достатньо входів для зовнішньої пам'яті");
                }
            }
            return new OperationDetails(result, resultMessages, "");
        }
            private OperationDetails CheckMotherBoardOnRAM(MotherBoard motherBoard, ComputerAssembly computerAssembly)
            {
                var result = true;
                var resultMessages = new List<string>();
                var bufferList = new List<ComputerAssemblyRAM>(computerAssembly.ComputerAssemblyRAMs);
                if(GetTotalRAM(computerAssembly) > motherBoard.MaxMemory)
                {
                    result = false;
                    resultMessages.Add("Материнська плата не підтримує таки обсяг ОЗП");
                }
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
                return new OperationDetails(result, resultMessages, "");
            }
            private OperationDetails CheckMotherBoardOnOuterMemory(MotherBoard motherBoard, ComputerAssembly computerAssembly)
            {
                var result = true;
                var resultMessages = new List<string>();
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

            var result = CheckHDD(hdd, computerAssembly);
            if (result.Succedeed)
            {
                var item = new ComputerAssemblyHDD();
                item.ComputerAssemblyId = computerAssembly.Id;
                item.HDDId = hdd.Id;
                Database.ComputerAssemblyHDDs.Create(item);
                Database.Save();
            }
            return result;
        }

        private OperationDetails CheckHDD(HDD hdd, ComputerAssembly computerAssembly)
        {
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
            if (computerAssembly.PowerSupply != null)
            {
                if (computerAssembly.PowerSupply.SATAInputNumber < computerAssembly.HDDs.Count() + computerAssembly.SSDs.Count() + 1)
                {
                    result = false;
                    resultMessages.Add("Блок не має достатньо входів для зовнішньої пам'яті");
                }
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
            var result = CheckPCCase(pcCase, computerAssembly);
            if (result.Succedeed)
            {
                computerAssembly.PCCaseId = pcCase.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            var details = result;
            details.MemoryFree = CalculateFreeOuterMemorySlot(computerAssembly);
            return details;
        }
        private OperationDetails CheckPCCase(PCCase pcCase, ComputerAssembly computerAssembly)
        {
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
            var bufferResult = CheckPCCaseOnOuterMemory(pcCase, computerAssembly);
            result = result && bufferResult.Succedeed;
            resultMessages = resultMessages.Concat(bufferResult.Messages).ToList();
            return new OperationDetails(result, resultMessages, "");
        }
        private OperationDetails CheckPCCaseOnOuterMemory(PCCase pcCase, ComputerAssembly computerAssembly)
        {
            var result = true;
            var resultMessages = new List<string>();
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

            return new OperationDetails(result, resultMessages, "");
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
            var result = CheckPowerSupply(powerSupply, computerAssembly);
            if (result.Succedeed) {
                computerAssembly.PowerSupplyId = powerSupply.Id;
                UpdateComputerAssembly(computerAssembly);
                Database.Save();
            }
            result.MemoryFree = CalculateFreeOuterMemorySlot(computerAssembly);
            return result;
        }
        private OperationDetails CheckPowerSupply (PowerSupply powerSupply, ComputerAssembly computerAssembly)
        {
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
            if (computerAssembly.HDDs.Count() > 0 || computerAssembly.SSDs.Count() > 0)
            {
                if (powerSupply.SATAInputNumber < computerAssembly.HDDs.Count() + computerAssembly.SSDs.Count())
                {
                    result = false;
                    resultMessages.Add("Блок не має достатньо входів для зовнішньої пам'яті");
                }
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
            var result = CheckVideoCard(videoCard, computerAssembly);
            if (result.Succedeed)
            {
                SetVideoCards(computerAssembly, new List<VideoCard> { videoCard });
                Database.Save();
            }
            return result;
        }
        private OperationDetails CheckVideoCard(VideoCard videoCard, ComputerAssembly computerAssembly)
        {
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
            var result = CheckRAM(ram, computerAssembly);
            if (result.Succedeed)
            {
                var item = new ComputerAssemblyRAM();
                item.ComputerAssemblyId = computerAssembly.Id;
                item.RAMId = ramId;
                Database.ComputerAssemblyRAMs.Create(item);
                Database.Save();
            }
            return result;
        }
        private OperationDetails CheckRAM(RAM ram, ComputerAssembly computerAssembly)
        {
            var result = true;
            var resultMessages = new List<string>();
            if (computerAssembly.MotherBoard != null)
            {
                if (GetTotalRAM(computerAssembly) + ram.MemorySize > computerAssembly.MotherBoard.MaxMemory)
                {
                    result = false;
                    resultMessages.Add("Материнська плата не підтримує такий обсяг ОЗП");
                }
                var freeSlots = GetFreeRamSlots(computerAssembly);
                if (!freeSlots.Any(m => m.RAMTypeId == ram.RAMTypeId))
                {
                    result = false;
                    resultMessages.Add("Материнська плата не містить входу для цієї оперативної памяті  (" + ram.RAMType.Name + ").");
                }
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
            result.MemoryFree = CalculateFreeOuterMemorySlot(assembly);
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
            result.MemoryFree = 1;
            //if (assembly.MotherBoard != null)
            //{
            //    assembly.MotherBoard = Database.MotherBoards.Get(assembly.MotherBoardId.Value);
            //    result.MemoryFree = assembly.MotherBoard.MotherBoardOuterMemorySlots.Count();
            //}
            result.MemoryFree = CalculateFreeOuterMemorySlot(assembly);
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
            var result = new OperationDetails(true, "Ok", "");
            result.MemoryFree = CalculateFreeOuterMemorySlot(assembly);
            return result;
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
            var result = new OperationDetails(true, "Ok", "");
            result.MemoryFree = CalculateFreeOuterMemorySlot(computerAssembly);
            return result;
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
            var result = new OperationDetails(true, "Ok", "");
            result.MemoryFree = CalculateFreeOuterMemorySlot(computerAssembly);
            return result;
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
            var result = new OperationDetails(true, "Ok", "");
            result.RamFree = CalculateFreeRAMSlot(computerAssembly);
            return result;
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
            var motherBoardSlots = 0;
            var pcCaseSlots = 0;
            var powerSupplySlots = 0;
            var freeSlots = 0;
            var calculatedItems = new List<int>();

            if (computerAssembly.MotherBoard != null)
            {
                computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                motherBoardSlots = computerAssembly.MotherBoard.MotherBoardOuterMemorySlots.Count();
                calculatedItems.Add(motherBoardSlots);
            }
            if (computerAssembly.PCCase != null)
            {
                computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                pcCaseSlots = computerAssembly.PCCase.PCCaseOuterMemoryFormFactors.Count();
                calculatedItems.Add(pcCaseSlots);
            }
            if(computerAssembly.PowerSupply != null)
            {
                powerSupplySlots = computerAssembly.PowerSupply.SATAInputNumber;
                calculatedItems.Add(powerSupplySlots);
            }
            //if (pcCaseSlots == -1 && motherBoardSlots >= 0)
            //{
            //    freeSlots = motherBoardSlots - (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count());
            //}
            //else if (pcCaseSlots >= 0 && motherBoardSlots == -1)
            //{
            //    freeSlots = pcCaseSlots - (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count());
            //}
            if (calculatedItems.Count() > 0)
            {
                freeSlots = calculatedItems.Min() - (computerAssembly.SSDs.Count() + computerAssembly.HDDs.Count());
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
            result = result.Concat(CreateRAMRecomendation(computerAssembly)).ToList();
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
            if(computerAssembly.MotherBoard == null)
            {
                var motherboards = Database.MotherBoards.GetAll();
                if(computerAssembly.CPUId != null)
                {
                    motherboards = motherboards.Where(m => m.CPUSocketId == computerAssembly.CPU.CPUSocketId);
                }
                if(computerAssembly.PCCase != null)
                {
                    computerAssembly.PCCase = Database.PCCases.Get(computerAssembly.PCCaseId.Value);
                    motherboards = motherboards.Where(m => computerAssembly.PCCase.PCCaseMotherBoardFormFactors.Any(z => z.MotherBoardFormFactorId == m.MotherBoardFormFactorId));
                }
                if(computerAssembly.PowerSupply != null)
                {
                    motherboards = motherboards.Where(m => computerAssembly.PowerSupply.PowerSupplyMotherBoardInterfaceId == m.MotherBoardFormFactorId).ToList();
                }
                if(computerAssembly.VideoCards.Count() > 0)
                {
                    var videoCard = computerAssembly.VideoCards.ElementAt(0);
                    motherboards = motherboards.Where(m => m.MotherBoardVideoCardSlots.Any(z => z.VideoCardInterfaceId == videoCard.VideoCard.VideoCardInterfaceId));
                }
                if(computerAssembly.ComputerAssemblyRAMs.Count() > 0)
                {
                    motherboards = motherboards.Where(m => CheckMotherBoardOnRAM(m, computerAssembly).Succedeed);
                    motherboards = motherboards.Where(m => m.MaxMemory > GetTotalRAM(computerAssembly));
                }
                if (computerAssembly.HDDs.Count() > 0 || computerAssembly.SSDs.Count() > 0)
                {
                    motherboards = motherboards.Where(m => CheckMotherBoardOnOuterMemory(m, computerAssembly).Succedeed).ToList();
                }
                result = motherboards.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "Материнська плата",
                    Name = m.Name,
                    ImagePath = "/Images/MotherBoard/" + m.Image,
                    Type = "MotherBoard"
                }).Take(3).ToList();

            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateSSDRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            if (CalculateFreeOuterMemorySlot(computerAssembly) > 0)
            {
                var ssds = Database.SSDs.GetAll();
                if (computerAssembly.MotherBoard != null)
                {
                    var freeSlots = GetFreeOuterSlotsForMotherBoard(computerAssembly);
                    ssds = ssds.Where(m => freeSlots.Any(z => z.OuterMemoryInterfaceId == m.OuterMemoryInterfaceId));
                }
                if (computerAssembly.PCCase != null)
                {
                    var freeSlots = GetFreeOuterSlotsForPCCase(computerAssembly);
                    ssds = ssds.Where(m => freeSlots.Any(z => z.OuterMemoryFormFactorId == m.OuterMemoryFormFactorId));
                }
                result = ssds.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "SSD",
                    Name = m.Name,
                    ImagePath = "/Images/SSD/" + m.Image,
                    Type = "SSD"
                }).Take(3).ToList();
            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateRAMRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            if (CalculateFreeRAMSlot(computerAssembly) > 0)
            {
                var rams = Database.RAMs.GetAll();
                if (computerAssembly.MotherBoard != null)
                {

                    var freeSlots = GetFreeRamSlots(computerAssembly);
                    rams = rams.Where(m => freeSlots.Any(z => z.RAMTypeId == m.RAMTypeId));
                    rams = rams.Where(m => m.MemorySize + GetTotalRAM(computerAssembly) < computerAssembly.MotherBoard.MaxMemory);
                }
                result = rams.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "RAM",
                    Name = m.Name,
                    ImagePath = "/Images/RAM/" + m.Image,
                    Type = "RAM"
                }).Take(2).ToList();
            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateHDDRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            if (CalculateFreeOuterMemorySlot(computerAssembly) > 0)
            {
                var hdds = Database.HDDs.GetAll();
                if (computerAssembly.MotherBoard != null)
                {
                    var freeSlots = GetFreeOuterSlotsForMotherBoard(computerAssembly);
                    hdds = hdds.Where(m => freeSlots.Any(z => z.OuterMemoryInterfaceId == m.OuterMemoryInterfaceId));
                }
                if (computerAssembly.PCCase != null)
                {
                    var freeSlots = GetFreeOuterSlotsForPCCase(computerAssembly);
                    hdds = hdds.Where(m => freeSlots.Any(z => z.OuterMemoryFormFactorId == m.OuterMemoryFormFactorId));
                }
                result = hdds.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "HDD",
                    Name = m.Name,
                    ImagePath = "/Images/HDD/" + m.Image,
                    Type = "HDD"
                }).Take(3).ToList();
            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreateVideCardRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            if (computerAssembly.VideoCards.Count() <= 0)
            {
                var videoCards = Database.VideoCards.GetAll();
                if (computerAssembly.PowerSupply != null)
                {
                    videoCards = videoCards.Where(m => m.MinimumPowerConsuming <= computerAssembly.PowerSupply.Power);
                }
                if (computerAssembly.MotherBoard != null)
                {
                    computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                    videoCards = videoCards.Where(m => computerAssembly.MotherBoard.MotherBoardVideoCardSlots.Any(z => z.VideoCardInterface.Version == m.VideoCardInterface.Version));
                }
                result = videoCards.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "Відео карта",
                    Name = m.Name,
                    ImagePath = "/Images/VideoCard/" + m.Image,
                    Type = "VideoCard"
                }).Take(3).ToList();
            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreatePowerSupplyRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            if(computerAssembly.PowerSupply == null)
            {
                var powerSupplies = Database.PowerSupplies.GetAll();
                if (computerAssembly.VideoCards.Count() > 0)
                {
                    var videoCard = computerAssembly.VideoCards.ElementAt(0);
                    powerSupplies = powerSupplies.Where(m => m.Power >= videoCard.VideoCard.MinimumPowerConsuming);
                }
                if (computerAssembly.MotherBoard != null)
                {
                    computerAssembly.MotherBoard = Database.MotherBoards.Get(computerAssembly.MotherBoardId.Value);
                    powerSupplies = powerSupplies.Where(m => computerAssembly.MotherBoard.PowerSupplyMotherBoardSlots.Any(z => z.PowerSupplyMotherBoardInterfaceId == m.PowerSupplyMotherBoardInterfaceId ));
                }
                if(computerAssembly.HDDs.Count() > 0 || computerAssembly.SSDs.Count() > 0)
                {
                    powerSupplies = powerSupplies.Where(m => m.SATAInputNumber >= computerAssembly.HDDs.Count() + computerAssembly.SSDs.Count());
                }
                result = powerSupplies.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "Блок живлення",
                    Name = m.Name,
                    ImagePath = "/Images/PowerSupply/" + m.Image,
                    Type = "PowerSupply"
                }).Take(3).ToList() ;
            }
            return result;
        }

        private IEnumerable<RecomendationDTO> CreatePCCaseRecomendation(ComputerAssembly computerAssembly)
        {
            var result = new List<RecomendationDTO>();
            if(computerAssembly.PCCase == null)
            {
                var pcCases = Database.PCCases.GetAll();
                if(computerAssembly.MotherBoard != null)
                {
                    pcCases = pcCases.Where(m => m.PCCaseMotherBoardFormFactors.Any(z => z.MotherBoardFormFactorId == computerAssembly.MotherBoard.MotherBoardFormFactorId));
                }
                if(computerAssembly.SSDs.Count() > 0 || computerAssembly.HDDs.Count() > 0)
                {
                    pcCases = pcCases.Where(m => CheckPCCaseOnOuterMemory(m, computerAssembly).Succedeed);
                }
                result = pcCases.Select(m => new RecomendationDTO()
                {
                    Id = m.Id,
                    Display = "Корпуси",
                    Name = m.Name,
                    ImagePath = "/Images/PCCase/" + m.Image,
                    Type = "PCCase"
                }).Take(3).ToList();
            }
            return result;
        }


       private bool IsFull(ComputerAssembly computerAssembly)
        {
            if(computerAssembly == null)
            {
                return false;
            }
            return computerAssembly.CPU != null && computerAssembly.MotherBoard != null && computerAssembly.PowerSupply != null
                && computerAssembly.PCCase != null && computerAssembly.VideoCards.Count() > 0
                && computerAssembly.ComputerAssemblyRAMs.Count() > 0 && (computerAssembly.HDDs.Count() + computerAssembly.SSDs.Count() > 0);
        }

        public OperationDetails OnRAMDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                foreach (var ssd in asseb.ComputerAssemblyRAMs)
                {
                    if (ssd.RAMId == id)
                    {
                        RemoveRam(asseb, id);
                    }
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnCPUDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if(asseb.CPUId == id)
                {
                    RemoveCPU(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnMotherBoardDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.MotherBoardId == id)
                {
                    RemoveMotherBoard(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnSSDDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                foreach(var ssd in asseb.SSDs)
                {
                    if (ssd.SSDId == id)
                    {
                        RemoveSSD(asseb, id);
                    }
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnHDDDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                foreach (var ssd in asseb.HDDs)
                {
                    if (ssd.HDDId == id)
                    {
                        RemoveHDD(asseb, id);
                    }
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnPCCaseDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.PCCaseId == id)
                {
                    RemovePCCase(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnPowerSupplyDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.PowerSupplyId == id)
                {
                    RemovePowerSupply(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnVideoCardDelete(int id)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                foreach (var ssd in asseb.VideoCards)
                {
                    if (ssd.VideoCardId == id)
                    {
                        RemoveVideoCard(asseb);
                    }
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnRAMChange(RAM ram)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                foreach (var ssd in asseb.ComputerAssemblyRAMs)
                {
                    if (ssd.RAMId == ram.Id && !CheckRAM(ram, asseb).Succedeed)
                    {
                        RemoveRam(asseb, ram.Id);
                    }
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnCPUChange(CPU cpu)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.CPUId == cpu.Id && !CheckCPU(cpu, asseb).Succedeed)
                {
                    RemoveCPU(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnMotherBoardChange(MotherBoard motherBoard)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.MotherBoardId == motherBoard.Id && !CheckMotherBoard(motherBoard, asseb).Succedeed)
                {
                    RemoveMotherBoard(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnSSDChange(SSD ssdd)
        {
                var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
                {
                    foreach (var ssd in asseb.SSDs)
                    {
                        if (ssd.SSDId == ssdd.Id && !CheckSSD(ssdd, asseb).Succedeed)
                        {
                            RemoveSSD(asseb, ssdd.Id);
                        }
                    }
                }

                return new OperationDetails(true, "", "");
            
        }

        public OperationDetails OnHDDChange(HDD hdd)
        { 
                var assemblies = GetComputerAssemblies().ToList();
                foreach (var asseb in assemblies)
                {
                    foreach (var ssd in asseb.HDDs)
                    {
                        if (ssd.HDDId == hdd.Id && !CheckHDD(hdd, asseb).Succedeed)
                        {
                            RemoveHDD(asseb, hdd.Id);
                        }
                    }
                }

                return new OperationDetails(true, "", "");
            }
        

        public OperationDetails OnPCCaseChange(PCCase pcaCase)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.PCCaseId == pcaCase.Id && !CheckPCCase(pcaCase, asseb).Succedeed)
                {
                    RemovePCCase(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnPowerSupplyChange(PowerSupply powerSupply)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                if (asseb.PowerSupplyId == powerSupply.Id && !CheckPowerSupply(powerSupply, asseb).Succedeed)
                {
                    RemovePowerSupply(asseb);
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails OnVideoCardChange(VideoCard videoCard)
        {
            var assemblies = GetComputerAssemblies().ToList();
            foreach (var asseb in assemblies)
            {
                foreach (var ssd in asseb.VideoCards)
                {
                    if (ssd.VideoCardId == videoCard.Id && !CheckVideoCard(videoCard, asseb).Succedeed)
                    {
                        RemoveVideoCard(asseb);
                    }
                }
            }

            return new OperationDetails(true, "", "");
        }

        public OperationDetails SoftwareSyncEvaluate(int id, ComputerAssembly computerAssembly)
        {
            var result = true;
            var messages = new List<string>();
            if (!IsFull(computerAssembly)){
                messages.Add("Щоб оцінити сумісність з ПЗ спочатку завершіть збірку");
                return new OperationDetails(false, messages, "");
            };
            var software = Database.Softwares.Get(id);
            if(software == null)
            {
                messages.Add("Виникла помилка ПЗ не знайдене.");
                return new OperationDetails(false, messages, "");
            }
            var minimumCPU = software.SoftwareCPURequirements.Where(m => m.RequirementTypeId == 1).Select(m => m.CPU);
            var requiredCPU = software.SoftwareCPURequirements.Where(m => m.RequirementTypeId == 2).Select(m => m.CPU);
            var minimumVideoCard = software.SoftwareVideoCardRequirements.Where(m => m.RequirementTypeId == 1).Select(m => m.VideoCard);
            var recomendedVideoCard = software.SoftwareVideoCardRequirements.Where(m => m.RequirementTypeId == 2).Select(m => m.VideoCard);
            if(minimumCPU.Count() <= 0 && requiredCPU.Count() <= 0)
            {
                messages.Add("Немає відомостей про вимоги до процесора.");
            }


            if(requiredCPU.Any(m => (m.Frequency <= computerAssembly.CPU.Frequency && m.ThreadsNumber <= computerAssembly.CPU.ThreadsNumber && m.CoresNumber <= computerAssembly.CPU.CoresNumber)))
            {
                messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp; Процесор " + computerAssembly.CPU.Name + " повністю задовільняє рекомендованим вимогам");
            }
            else
            {
                if (!requiredCPU.Any(m => (m.Frequency <= computerAssembly.CPU.Frequency)))
                {
                    messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " має недостатню частоту для рекомендованих вимог");
                }
                if (!requiredCPU.Any(m => (m.ThreadsNumber <= computerAssembly.CPU.ThreadsNumber)))
                {
                    messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " має недостатню кількість потоків для рекомендованих вимог");
                }
                if (!requiredCPU.Any(m => (m.CoresNumber <= computerAssembly.CPU.CoresNumber)))
                {
                    messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " має недостатню кількість ядер для рекомендованих вимог");
                }
                if (minimumCPU.Any(m => (m.Frequency <= computerAssembly.CPU.Frequency && m.ThreadsNumber <= computerAssembly.CPU.ThreadsNumber && m.CoresNumber <= computerAssembly.CPU.CoresNumber)))
                {
                    messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " повністю задовільняє мінімальним вимогам");
                }
                else
                {
                    if (!minimumCPU.Any(m => (m.Frequency <= computerAssembly.CPU.Frequency)))
                    {
                        messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " має недостатню частоту для мінімальних вимог");
                    }
                    if (!minimumCPU.Any(m => (m.ThreadsNumber <= computerAssembly.CPU.ThreadsNumber)))
                    {
                        messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " має недостатню кількість потоків для мінімальних вимог");
                    }
                    if (!minimumCPU.Any(m => (m.CoresNumber <= computerAssembly.CPU.CoresNumber)))
                    {
                        messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Процесор " + computerAssembly.CPU.Name + " має недостатню кількість ядер для мінімальних   вимог");
                    }
                }
            }
            



            if (minimumVideoCard.Count() <= 0 && recomendedVideoCard.Count() <= 0)
            {
                messages.Add("Немає відомостей про вимоги до відеокарти.");
            }


            if (recomendedVideoCard.Any(m => (m.MemorySize <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemorySize && m.Frequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.Frequency && m.MemoryFrequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemoryFrequency)))
            {
                messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " повністю задовільняє рекомендованим вимогам");
            }
            else
            {
                if (!recomendedVideoCard.Any(m => (m.Frequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.Frequency)))
                {
                    messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " має недостатню частоту ядра для рекомендованих вимог");
                }
                if (!recomendedVideoCard.Any(m => (m.MemoryFrequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemoryFrequency)))
                {
                    messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " має недостатню частоту пам'яті для рекомендованих вимог");
                }
                if (!recomendedVideoCard.Any(m => (m.MemorySize <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemorySize)))
                {
                    messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " має недостатній обсяг відео пам'яті для рекомендованих вимог");
                }
                if (minimumVideoCard.Any(m => (m.MemorySize <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemorySize && m.Frequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.Frequency && m.MemoryFrequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemoryFrequency)))
                {
                    messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " повністю задовільняє мінімальним вимогам");
                }
                else
                {
                    if (!minimumVideoCard.Any(m => (m.Frequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.Frequency)))
                    {
                        messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " має недостатню частоту ядра для мінімальних вимог");
                    }
                    if (!minimumVideoCard.Any(m => (m.MemoryFrequency <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemoryFrequency)))
                    {
                        messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " має недостатню частоту пам'яті для мінімальних вимог");
                    }
                    if (!minimumVideoCard.Any(m => (m.MemorySize <= computerAssembly.VideoCards.ElementAt(0).VideoCard.MemorySize)))
                    {
                        messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Відеокарта " + computerAssembly.VideoCards.ElementAt(0).VideoCard.Name + " має недостатній обсяг відео пам'яті для мінімальних вимог");
                    }
                }
            }


            


            if (software.RecommendedRequiredRAM <= GetTotalRAM(computerAssembly))
            {
                messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp;Збірка має достатньо ОЗУ для рекомендованих вимог");
            } else if(software.MinimiumRequiredRAM <= GetTotalRAM(computerAssembly))
            {
                messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp;Збірка має достатньо ОЗУ для мінімальних вимог");
            } else
            {
                messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Збірка має недостатньо ОЗУ для підтримки ПЗ");
            }



            if (software.DiscVolume / 1000 <= GetTotalVolume(computerAssembly))
            {
                messages.Add("<i class='fa fa-check' aria-hidden='true'></i>&nbsp;Збірка має достатньо памяті для зберігання ПЗ");
            }
            else
            {
                messages.Add("<i class='fa fa-ban' aria-hidden='true'></i>&nbsp;Збірка не має достатньо памяті для зберігання ПЗ");
            }

            return new OperationDetails(result, messages, "");

        }

        public OperationDetails compareCPU(CPU cpu1, CPU cpu2)
        {
            var result = true;
            var messages = new List<string>();

            if (cpu1.CoresNumber < cpu2.CoresNumber || cpu1.Frequency < cpu2.Frequency || cpu1.ThreadsNumber < cpu2.ThreadsNumber)
            {
                result = false;
            }

            return new OperationDetails(result, "", "");
        }


        public OperationDetails compareVideoCards(VideoCard videoCard1, VideoCard videoCard2)
        {
            var result = true;
            var messages = new List<string>();

            if (videoCard1.MemoryFrequency < videoCard2.MemoryFrequency || videoCard1.Frequency < videoCard2.Frequency || videoCard1.MemorySize < videoCard2.MemorySize)
            {
                result = false;
            }

            return new OperationDetails(result, "", "");
        }
        public int GetTotalRAM(ComputerAssembly computerAssembly)
        {
            var sum = 0;
            foreach (var ram in computerAssembly.ComputerAssemblyRAMs)
            {
                sum += ram.RAM.MemorySize;
            }

            return sum;
        }

        public int GetTotalVolume(ComputerAssembly computerAssembly)
        {
            var sum = 0;
            foreach(var ssd in computerAssembly.SSDs)
            {
                sum += ssd.SSD.MemorySize;
            }
            foreach (var hdd in computerAssembly.HDDs)
            {
                sum += hdd.HDD.MemorySize;
            }

            return sum;
        }

        public decimal CalculatePrice(ComputerAssembly computerAssembly)
        {
            decimal price = 0;
            if(computerAssembly.CPU != null)
            {
                price += computerAssembly.CPU.Price;
            }
            if (computerAssembly.MotherBoard != null)
            {
                price += computerAssembly.MotherBoard.Price;
            }
            if(computerAssembly.PowerSupply != null)
            {
                price += computerAssembly.PowerSupply.Price;
            }
            if(computerAssembly.PCCase != null)
            {
                price += computerAssembly.PCCase.Price;
            }
            if(computerAssembly.VideoCards.Count() > 0)
            {
                foreach(var videoCard in computerAssembly.VideoCards)
                {
                    price += videoCard.VideoCard.Price;
                }
            }
            if (computerAssembly.ComputerAssemblyRAMs.Count() > 0)
            {
                foreach (var ram in computerAssembly.ComputerAssemblyRAMs)
                {
                    price += ram.RAM.Price;
                }
            }
            if (computerAssembly.SSDs.Count() > 0)
            {
                foreach (var ssd in computerAssembly.SSDs)
                {
                    price += ssd.SSD.Price;
                }
            }
            if (computerAssembly.HDDs.Count() > 0)
            {
                foreach (var hdd in computerAssembly.HDDs)
                {
                    price += hdd.HDD.Price;
                }
            }

            return price;
        }
    }
}
