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
    public class MotherBoardFormFactorRepository : IRepository<MotherBoardFormFactor>
    {
        private ApplicationContext db;

        public MotherBoardFormFactorRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<MotherBoardFormFactor> GetAll()
        {
            return db.MotherBoardFormFactors;
        }

        public MotherBoardFormFactor Get(int id)
        {
            return db.MotherBoardFormFactors.Find(id);
        }

        public void Create(MotherBoardFormFactor motherBoardFormFactor)
        {
            db.MotherBoardFormFactors.Add(motherBoardFormFactor);
        }

        public void Update(MotherBoardFormFactor motherBoardFormFactor)
        {
            db.Entry(motherBoardFormFactor).State = EntityState.Modified;
        }
        public IEnumerable<MotherBoardFormFactor> Find(Func<MotherBoardFormFactor, Boolean> predicate)
        {
            return db.MotherBoardFormFactors.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            MotherBoardFormFactor motherBoardFormFactor = db.MotherBoardFormFactors.Find(id);
            if (motherBoardFormFactor != null)
                db.MotherBoardFormFactors.Remove(motherBoardFormFactor);
        }
    }
}
