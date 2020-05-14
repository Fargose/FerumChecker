using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.DataAccess.Entities.Specification;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories.Specification
{
    public class PowerSupplyCPUInterfaceRepository : IRepository<PowerSupplyCPUInterface>
    {
        private ApplicationContext db;

        public PowerSupplyCPUInterfaceRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PowerSupplyCPUInterface> GetAll()
        {
            return db.PowerSupplyCPUInterfaces;
        }

        public PowerSupplyCPUInterface Get(int id)
        {
            return db.PowerSupplyCPUInterfaces.Find(id);
        }

        public void Create(PowerSupplyCPUInterface powerSupplyCPUInterface)
        {
            db.PowerSupplyCPUInterfaces.Add(powerSupplyCPUInterface);
        }

        public void Update(PowerSupplyCPUInterface powerSupplyCPUInterface)
        {
            db.Entry(powerSupplyCPUInterface).State = EntityState.Modified;
        }
        public IEnumerable<PowerSupplyCPUInterface> Find(Func<PowerSupplyCPUInterface, Boolean> predicate)
        {
            return db.PowerSupplyCPUInterfaces.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PowerSupplyCPUInterface powerSupplyCPUInterface = db.PowerSupplyCPUInterfaces.Find(id);
            if (powerSupplyCPUInterface != null)
                db.PowerSupplyCPUInterfaces.Remove(powerSupplyCPUInterface);
        }
    }
}
