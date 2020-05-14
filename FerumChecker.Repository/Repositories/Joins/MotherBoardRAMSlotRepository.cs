using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories.Joins
{
    class MotherBoardRAMSlotRepository: IRepository<MotherBoardRAMSlot>
    {
        private ApplicationContext db;

        public MotherBoardRAMSlotRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardRAMSlot> GetAll()
        {
            return db.MotherBoardRAMSlots;
        }

        public MotherBoardRAMSlot Get(int id)
        {
            return db.MotherBoardRAMSlots.Find(id);
        }

        public void Create(MotherBoardRAMSlot motherBoardRAMSlot)
        {
            db.MotherBoardRAMSlots.Add(motherBoardRAMSlot);
        }

        public void Update(MotherBoardRAMSlot motherBoardRAMSlot)
        {
            db.Entry(motherBoardRAMSlot).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardRAMSlot> Find(Func<MotherBoardRAMSlot, Boolean> predicate)
        {
            return db.MotherBoardRAMSlots.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardRAMSlot motherBoardRAMSlot = db.MotherBoardRAMSlots.Find(id);
            if (motherBoardRAMSlot != null)
                db.MotherBoardRAMSlots.Remove(motherBoardRAMSlot);
        }
    }
}
