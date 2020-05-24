using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.Repository.Interfaces;
using FerumChecker.Service.Infrastructure;
using FerumChecker.Service.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Service.Services.Hardware
{
    public class MotherBoardService : IMotherBoardService
    {

        IUnitOfWork Database { get; set; }

        public MotherBoardService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public MotherBoard GetMotherBoard(int? id)
        {
            return Database.MotherBoards.Get(id.Value);
        }

        public IEnumerable<MotherBoard> GetMotherBoards()
        {
            return Database.MotherBoards.GetAll();
        }

        public OperationDetails UpdateMotherBoard(MotherBoard motherBoard)
        {
            
            Database.MotherBoards.Update(motherBoard);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails CreateMotherBoard(MotherBoard motherBoard)
        {
            Database.MotherBoards.Create(motherBoard);
            SetOuterMemoryInterfaces(motherBoard, (List<MotherBoardOuterMemorySlot>)motherBoard.MotherBoardOuterMemorySlots);
            SetVideoCardInterfaces(motherBoard, (List<MotherBoardVideoCardSlot>)motherBoard.MotherBoardVideoCardSlots);
            SetPowerSupplyInterfaces(motherBoard, (List<MotherBoardPowerSupplySlot>)motherBoard.PowerSupplyMotherBoardSlots);
            SetRAMSlots(motherBoard, (List<MotherBoardRAMSlot>)motherBoard.MotherBoardRAMSlots);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        public OperationDetails DeleteMotherBoard(int? id)
        {
            Database.MotherBoards.Delete(id.Value);
            Database.Save();
            return new OperationDetails(true, "Ok", "");
        }

        private OperationDetails SetVideoCardInterfaces(MotherBoard motherBoard, List<MotherBoardVideoCardSlot> videoCards)
        {
            if(motherBoard.Id > -1 && videoCards != null)
            {
                foreach(var item in videoCards)
                {
                    item.MotherBoardId = motherBoard.Id;
                    this.Database.MotherBoardVideoCardSlots.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }


        private OperationDetails SetOuterMemoryInterfaces(MotherBoard motherBoard, List<MotherBoardOuterMemorySlot> outerMemories)
        {
            if (motherBoard.Id > -1 && outerMemories != null)
            {
                foreach (var item in outerMemories)
                {
                    item.MotherBoardId = motherBoard.Id;
                    this.Database.MotherBoardOuterMemorySlots.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }


        private OperationDetails SetRAMSlots(MotherBoard motherBoard, List<MotherBoardRAMSlot> rams)
        {
            if (motherBoard.Id > -1 && rams != null)
            {
                foreach (var item in rams)
                {
                    item.MotherBoardId = motherBoard.Id;
                    this.Database.MotherBoardRAMSlots.Create(item);
                }
            }

            return new OperationDetails(true, "Ok", "");
        }


        private OperationDetails SetPowerSupplyInterfaces(MotherBoard motherBoard, List<MotherBoardPowerSupplySlot> powerSuppliesSlots)
        {
            if (motherBoard.Id > -1 && powerSuppliesSlots != null)
            {
                foreach (var item in powerSuppliesSlots)
                {
                    item.MotherBoardId = motherBoard.Id;
                    this.Database.MotherBoardPowerSupplySlots.Create(item);
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
