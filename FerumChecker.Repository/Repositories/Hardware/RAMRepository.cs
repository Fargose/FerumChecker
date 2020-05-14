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
    class RAMRepository : IRepository<RAM>
    {
        private ApplicationContext db;

        public RAMRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<RAM> GetAll()
        {
            return db.RAMs;
        }

        public RAM Get(int id)
        {
            return db.RAMs.Find(id);
        }

        public void Create(RAM ram)
        {
            db.RAMs.Add(ram);
        }

        public void Update(RAM ram)
        {
            db.Entry(ram).State = EntityState.Modified;
        }
        public IEnumerable<RAM> Find(Func<RAM, Boolean> predicate)
        {
            return db.RAMs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            RAM ram = db.RAMs.Find(id);
            if (ram != null)
                db.RAMs.Remove(ram);
        }
    }
}
