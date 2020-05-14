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
    class MotherBoardVideoCardSlotRepository: IRepository<MotherBoardVideoCardSlot>
    {
        private ApplicationContext db;

        public MotherBoardVideoCardSlotRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardVideoCardSlot> GetAll()
        {
            return db.MotherBoardVideoCardSlots;
        }

        public MotherBoardVideoCardSlot Get(int id)
        {
            return db.MotherBoardVideoCardSlots.Find(id);
        }

        public void Create(MotherBoardVideoCardSlot motherBoardVideoCardSlot)
        {
            db.MotherBoardVideoCardSlots.Add(motherBoardVideoCardSlot);
        }

        public void Update(MotherBoardVideoCardSlot motherBoardVideoCardSlot)
        {
            db.Entry(motherBoardVideoCardSlot).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardVideoCardSlot> Find(Func<MotherBoardVideoCardSlot, Boolean> predicate)
        {
            return db.MotherBoardVideoCardSlots.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardVideoCardSlot motherBoardVideoCardSlot = db.MotherBoardVideoCardSlots.Find(id);
            if (motherBoardVideoCardSlot != null)
                db.MotherBoardVideoCardSlots.Remove(motherBoardVideoCardSlot);
        }
    }
}
