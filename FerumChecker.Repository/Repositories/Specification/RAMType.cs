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
    public class RAMTypeRepository : IRepository<RAMType>
    {
        private ApplicationContext db;

        public RAMTypeRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<RAMType> GetAll()
        {
            return db.RAMTypes;
        }

        public RAMType Get(int id)
        {
            return db.RAMTypes.Find(id);
        }

        public void Create(RAMType ramType)
        {
            db.RAMTypes.Add(ramType);
        }

        public void Update(RAMType ramType)
        {
            db.Entry(ramType).State = EntityState.Modified;
        }
        public IEnumerable<RAMType> Find(Func<RAMType, Boolean> predicate)
        {
            return db.RAMTypes.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            RAMType ramType = db.RAMTypes.Find(id);
            if (ramType != null)
                db.RAMTypes.Remove(ramType);
        }
    }
}
