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
    public class PowerSupplyMotherBoardInterfaceRepository : IRepository<PowerSupplyMotherBoardInterface>
    {
        private ApplicationContext db;

        public PowerSupplyMotherBoardInterfaceRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PowerSupplyMotherBoardInterface> GetAll()
        {
            return db.PowerSupplyMotherBoardInterfaces;
        }

        public PowerSupplyMotherBoardInterface Get(int id)
        {
            return db.PowerSupplyMotherBoardInterfaces.Find(id);
        }

        public void Create(PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface)
        {
            db.PowerSupplyMotherBoardInterfaces.Add(powerSupplyMotherBoardInterface);
        }

        public void Update(PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface)
        {
            db.Entry(powerSupplyMotherBoardInterface).State = EntityState.Modified;
        }
        public IEnumerable<PowerSupplyMotherBoardInterface> Find(Func<PowerSupplyMotherBoardInterface, Boolean> predicate)
        {
            return db.PowerSupplyMotherBoardInterfaces.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PowerSupplyMotherBoardInterface powerSupplyMotherBoardInterface = db.PowerSupplyMotherBoardInterfaces.Find(id);
            if (powerSupplyMotherBoardInterface != null)
                db.PowerSupplyMotherBoardInterfaces.Remove(powerSupplyMotherBoardInterface);
        }
    }
}
