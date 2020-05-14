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
    class PowerSupplyRepository : IRepository<PowerSupply>
    {
        private ApplicationContext db;

        public PowerSupplyRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PowerSupply> GetAll()
        {
            return db.PowerSupplies;
        }

        public PowerSupply Get(int id)
        {
            return db.PowerSupplies.Find(id);
        }

        public void Create(PowerSupply powerSupply)
        {
            db.PowerSupplies.Add(powerSupply);
        }

        public void Update(PowerSupply powerSupply)
        {
            db.Entry(powerSupply).State = EntityState.Modified;
        }
        public IEnumerable<PowerSupply> Find(Func<PowerSupply, Boolean> predicate)
        {
            return db.PowerSupplies.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PowerSupply powerSupply = db.PowerSupplies.Find(id);
            if (powerSupply != null)
                db.PowerSupplies.Remove(powerSupply);
        }
    }
}
