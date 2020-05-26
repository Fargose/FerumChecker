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
    class PCCaseRepository : IRepository<PCCase>
    {
        private ApplicationContext db;

        public PCCaseRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PCCase> GetAll()
        {
            return db.PCCases
                .Include(m => m.Manufacturer)
                .Include(m => m.PCCaseOuterMemoryFormFactors).ThenInclude(m => m.OuterMemoryFormFactors)
                .Include(m => m.PCCaseMotherBoardFormFactors).ThenInclude(m => m.MotherBoardFormFactor);
        }

        public PCCase Get(int id)
        {
            return db.PCCases.Include(m => m.Manufacturer)
                .Include(m => m.PCCaseOuterMemoryFormFactors).ThenInclude(m => m.OuterMemoryFormFactors)
                .Include(m => m.PCCaseMotherBoardFormFactors).ThenInclude(m => m.MotherBoardFormFactor).FirstOrDefault(m => m.Id == id);
        }

        public void Create(PCCase pcCase)
        {
            db.PCCases.Add(pcCase);
        }

        public void Update(PCCase pcCase)
        {
            db.Entry(pcCase).State = EntityState.Modified;
        }
        public IEnumerable<PCCase> Find(Func<PCCase, Boolean> predicate)
        {
            return db.PCCases.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PCCase pcCase = db.PCCases.Find(id);
            if (pcCase != null)
                db.PCCases.Remove(pcCase);
        }
    }
}
