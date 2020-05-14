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
    class PCCaseOuterMemoryFormFactorRepository: IRepository<PCCaseOuterMemoryFormFactor>
    {
        private ApplicationContext db;

        public PCCaseOuterMemoryFormFactorRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PCCaseOuterMemoryFormFactor> GetAll()
        {
            return db.PCCaseOuterMemoryFormFactors;
        }

        public PCCaseOuterMemoryFormFactor Get(int id)
        {
            return db.PCCaseOuterMemoryFormFactors.Find(id);
        }

        public void Create(PCCaseOuterMemoryFormFactor pcCaseOuterMemoryFormFactor)
        {
            db.PCCaseOuterMemoryFormFactors.Add(pcCaseOuterMemoryFormFactor);
        }

        public void Update(PCCaseOuterMemoryFormFactor pcCaseOuterMemoryFormFactor)
        {
            db.Entry(pcCaseOuterMemoryFormFactor).State = EntityState.Modified;
        }
        public IEnumerable<PCCaseOuterMemoryFormFactor> Find(Func<PCCaseOuterMemoryFormFactor, Boolean> predicate)
        {
            return db.PCCaseOuterMemoryFormFactors.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PCCaseOuterMemoryFormFactor pcCaseOuterMemoryFormFactor = db.PCCaseOuterMemoryFormFactors.Find(id);
            if (pcCaseOuterMemoryFormFactor != null)
                db.PCCaseOuterMemoryFormFactors.Remove(pcCaseOuterMemoryFormFactor);
        }
    }
}
