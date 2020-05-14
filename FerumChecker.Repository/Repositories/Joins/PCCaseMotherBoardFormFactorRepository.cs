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
    class PCCaseMotherBoardFormFactorRepository: IRepository<PCCaseMotherBoardFormFactor>
    {
        private ApplicationContext db;

        public PCCaseMotherBoardFormFactorRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PCCaseMotherBoardFormFactor> GetAll()
        {
            return db.PCCaseMotherBoardFormFactors;
        }

        public PCCaseMotherBoardFormFactor Get(int id)
        {
            return db.PCCaseMotherBoardFormFactors.Find(id);
        }

        public void Create(PCCaseMotherBoardFormFactor pcCaseMotherBoardFormFactor)
        {
            db.PCCaseMotherBoardFormFactors.Add(pcCaseMotherBoardFormFactor);
        }

        public void Update(PCCaseMotherBoardFormFactor pcCaseMotherBoardFormFactor)
        {
            db.Entry(pcCaseMotherBoardFormFactor).State = EntityState.Modified;
        }
        public IEnumerable<PCCaseMotherBoardFormFactor> Find(Func<PCCaseMotherBoardFormFactor, Boolean> predicate)
        {
            return db.PCCaseMotherBoardFormFactors.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PCCaseMotherBoardFormFactor pcCaseMotherBoardFormFactor = db.PCCaseMotherBoardFormFactors.Find(id);
            if (pcCaseMotherBoardFormFactor != null)
                db.PCCaseMotherBoardFormFactors.Remove(pcCaseMotherBoardFormFactor);
        }
    }
}
