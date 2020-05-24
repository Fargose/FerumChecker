using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Hardware;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories.Hardware
{
    class MotherBoardRepository : IRepository<MotherBoard>
    {
        private ApplicationContext db;

        public MotherBoardRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoard> GetAll()
        {
            return db.MotherBoards
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MotherBoardFormFactor)
                .Include(m => m.MotherBoardNothernBridge);
        }

        public MotherBoard Get(int id)
        {
            return db.MotherBoards
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MotherBoardFormFactor)
                .Include(m => m.MotherBoardNothernBridge)
                .Include(m => m.MotherBoardOuterMemorySlots).ThenInclude(m => m.OuterMemoryInterface)
                .Include(m => m.MotherBoardVideoCardSlots).ThenInclude(m => m.VideoCardInterface)
                .Include(m => m.MotherBoardRAMSlots).ThenInclude(m => m.RAMType)
                .Include(m => m.PowerSupplyMotherBoardSlots).ThenInclude(m => m.PowerSupplyMotherBoardInterface)
                .FirstOrDefault(m => m.Id == id);
        }

        public void Create(MotherBoard motherBoard)
        {
            db.MotherBoards.Add(motherBoard);
        }

        public void Update(MotherBoard motherBoard)
        {
            db.Entry(motherBoard).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoard> Find(Func<MotherBoard, Boolean> predicate)
        {
            return db.MotherBoards.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoard motherBoard = db.MotherBoards.Find(id);
            if (motherBoard != null)
                db.MotherBoards.Remove(motherBoard);
        }
    }
}
