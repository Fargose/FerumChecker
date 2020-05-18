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
    public class MotherBoardNorthBridgeRepository : IRepository<MotherBoardNorthBridge>
    {
        private ApplicationContext db;

        public MotherBoardNorthBridgeRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardNorthBridge> GetAll()
        {
            return db.MotherBoardNorthBridges;
        }

        public MotherBoardNorthBridge Get(int id)
        {
            return db.MotherBoardNorthBridges.Find(id);
        }

        public void Create(MotherBoardNorthBridge motherBoardNothernBridge)
        {
            db.MotherBoardNorthBridges.Add(motherBoardNothernBridge);
        }

        public void Update(MotherBoardNorthBridge motherBoardNothernBridge)
        {
            db.Entry(motherBoardNothernBridge).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardNorthBridge> Find(Func<MotherBoardNorthBridge, Boolean> predicate)
        {
            return db.MotherBoardNorthBridges.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardNorthBridge motherBoardNothernBridge = db.MotherBoardNorthBridges.Find(id);
            if (motherBoardNothernBridge != null)
                db.MotherBoardNorthBridges.Remove(motherBoardNothernBridge);
        }
    }
}
