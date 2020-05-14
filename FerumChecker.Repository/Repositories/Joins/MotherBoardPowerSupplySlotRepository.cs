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
    class MotherBoardPowerSupplySlotRepository: IRepository<MotherBoardPowerSupplySlot>
    {
        private ApplicationContext db;

        public MotherBoardPowerSupplySlotRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardPowerSupplySlot> GetAll()
        {
            return db.MotherBoardPowerSupplySlots;
        }

        public MotherBoardPowerSupplySlot Get(int id)
        {
            return db.MotherBoardPowerSupplySlots.Find(id);
        }

        public void Create(MotherBoardPowerSupplySlot motherBoardPowerSupplySlot)
        {
            db.MotherBoardPowerSupplySlots.Add(motherBoardPowerSupplySlot);
        }

        public void Update(MotherBoardPowerSupplySlot motherBoardPowerSupplySlot)
        {
            db.Entry(motherBoardPowerSupplySlot).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardPowerSupplySlot> Find(Func<MotherBoardPowerSupplySlot, Boolean> predicate)
        {
            return db.MotherBoardPowerSupplySlots.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardPowerSupplySlot motherBoardPowerSupplySlot = db.MotherBoardPowerSupplySlots.Find(id);
            if (motherBoardPowerSupplySlot != null)
                db.MotherBoardPowerSupplySlots.Remove(motherBoardPowerSupplySlot);
        }
    }
}
