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
    class PowerSupplyPowerSupplyCPUInterfaceRepository: IRepository<PowerSupplyPowerSupplyCPUInterface>
    {
        private ApplicationContext db;

        public PowerSupplyPowerSupplyCPUInterfaceRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PowerSupplyPowerSupplyCPUInterface> GetAll()
        {
            return db.PowerSupplyPowerSupplyCPUInterfaces;
        }

        public PowerSupplyPowerSupplyCPUInterface Get(int id)
        {
            return db.PowerSupplyPowerSupplyCPUInterfaces.Find(id);
        }

        public void Create(PowerSupplyPowerSupplyCPUInterface powerSupplyPowerSupplyCPUInterface)
        {
            db.PowerSupplyPowerSupplyCPUInterfaces.Add(powerSupplyPowerSupplyCPUInterface);
        }

        public void Update(PowerSupplyPowerSupplyCPUInterface powerSupplyPowerSupplyCPUInterface)
        {
            db.Entry(powerSupplyPowerSupplyCPUInterface).State = EntityState.Modified;
        }
        public IEnumerable<PowerSupplyPowerSupplyCPUInterface> Find(Func<PowerSupplyPowerSupplyCPUInterface, Boolean> predicate)
        {
            return db.PowerSupplyPowerSupplyCPUInterfaces.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PowerSupplyPowerSupplyCPUInterface powerSupplyPowerSupplyCPUInterface = db.PowerSupplyPowerSupplyCPUInterfaces.Find(id);
            if (powerSupplyPowerSupplyCPUInterface != null)
                db.PowerSupplyPowerSupplyCPUInterfaces.Remove(powerSupplyPowerSupplyCPUInterface);
        }
    }
}
