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
    public class MotherBoardNothernBridgeRepository : IRepository<MotherBoardNothernBridge>
    {
        private ApplicationContext db;

        public MotherBoardNothernBridgeRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardNothernBridge> GetAll()
        {
            return db.MotherBoardNothernBridges;
        }

        public MotherBoardNothernBridge Get(int id)
        {
            return db.MotherBoardNothernBridges.Find(id);
        }

        public void Create(MotherBoardNothernBridge motherBoardNothernBridge)
        {
            db.MotherBoardNothernBridges.Add(motherBoardNothernBridge);
        }

        public void Update(MotherBoardNothernBridge motherBoardNothernBridge)
        {
            db.Entry(motherBoardNothernBridge).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardNothernBridge> Find(Func<MotherBoardNothernBridge, Boolean> predicate)
        {
            return db.MotherBoardNothernBridges.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardNothernBridge motherBoardNothernBridge = db.MotherBoardNothernBridges.Find(id);
            if (motherBoardNothernBridge != null)
                db.MotherBoardNothernBridges.Remove(motherBoardNothernBridge);
        }
    }
}
