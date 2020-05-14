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
    public class OuterMemoryFormFactorRepository : IRepository<OuterMemoryFormFactor>
    {
        private ApplicationContext db;

        public OuterMemoryFormFactorRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<OuterMemoryFormFactor> GetAll()
        {
            return db.OuterMemoryFormFactors;
        }

        public OuterMemoryFormFactor Get(int id)
        {
            return db.OuterMemoryFormFactors.Find(id);
        }

        public void Create(OuterMemoryFormFactor outerMemoryFormFactor)
        {
            db.OuterMemoryFormFactors.Add(outerMemoryFormFactor);
        }

        public void Update(OuterMemoryFormFactor outerMemoryFormFactor)
        {
            db.Entry(outerMemoryFormFactor).State = EntityState.Modified;
        }
        public IEnumerable<OuterMemoryFormFactor> Find(Func<OuterMemoryFormFactor, Boolean> predicate)
        {
            return db.OuterMemoryFormFactors.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            OuterMemoryFormFactor outerMemoryFormFactor = db.OuterMemoryFormFactors.Find(id);
            if (outerMemoryFormFactor != null)
                db.OuterMemoryFormFactors.Remove(outerMemoryFormFactor);
        }
    }
}
