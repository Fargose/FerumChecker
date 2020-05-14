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
    class MotherBoardOuterMemorySlotRepository: IRepository<MotherBoardOuterMemorySlot>
    {
        private ApplicationContext db;

        public MotherBoardOuterMemorySlotRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardOuterMemorySlot> GetAll()
        {
            return db.MotherBoardOuterMemorySlots;
        }

        public MotherBoardOuterMemorySlot Get(int id)
        {
            return db.MotherBoardOuterMemorySlots.Find(id);
        }

        public void Create(MotherBoardOuterMemorySlot motherBoardOuterMemorySlot)
        {
            db.MotherBoardOuterMemorySlots.Add(motherBoardOuterMemorySlot);
        }

        public void Update(MotherBoardOuterMemorySlot motherBoardOuterMemorySlot)
        {
            db.Entry(motherBoardOuterMemorySlot).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardOuterMemorySlot> Find(Func<MotherBoardOuterMemorySlot, Boolean> predicate)
        {
            return db.MotherBoardOuterMemorySlots.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardOuterMemorySlot motherBoardOuterMemorySlot = db.MotherBoardOuterMemorySlots.Find(id);
            if (motherBoardOuterMemorySlot != null)
                db.MotherBoardOuterMemorySlots.Remove(motherBoardOuterMemorySlot);
        }
    }
}
