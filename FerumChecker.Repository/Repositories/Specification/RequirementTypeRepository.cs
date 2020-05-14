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
    public class RequirementTypeRepository : IRepository<RequirementType>
    {
        private ApplicationContext db;

        public RequirementTypeRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<RequirementType> GetAll()
        {
            return db.RequirementTypes;
        }

        public RequirementType Get(int id)
        {
            return db.RequirementTypes.Find(id);
        }

        public void Create(RequirementType requirementType)
        {
            db.RequirementTypes.Add(requirementType);
        }

        public void Update(RequirementType requirementType)
        {
            db.Entry(requirementType).State = EntityState.Modified;
        }
        public IEnumerable<RequirementType> Find(Func<RequirementType, Boolean> predicate)
        {
            return db.RequirementTypes.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            RequirementType requirementType = db.RequirementTypes.Find(id);
            if (requirementType != null)
                db.RequirementTypes.Remove(requirementType);
        }
    }
}
