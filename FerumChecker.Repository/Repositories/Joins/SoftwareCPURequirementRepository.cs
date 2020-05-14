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
    class SoftwareCPURequirementRepository: IRepository<SoftwareCPURequirement>
    {
        private ApplicationContext db;

        public SoftwareCPURequirementRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<SoftwareCPURequirement> GetAll()
        {
            return db.SoftwareCPURequirements;
        }

        public SoftwareCPURequirement Get(int id)
        {
            return db.SoftwareCPURequirements.Find(id);
        }

        public void Create(SoftwareCPURequirement softwareCPURequirement)
        {
            db.SoftwareCPURequirements.Add(softwareCPURequirement);
        }

        public void Update(SoftwareCPURequirement softwareCPURequirement)
        {
            db.Entry(softwareCPURequirement).State = EntityState.Modified;
        }
        public IEnumerable<SoftwareCPURequirement> Find(Func<SoftwareCPURequirement, Boolean> predicate)
        {
            return db.SoftwareCPURequirements.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            SoftwareCPURequirement softwareCPURequirement = db.SoftwareCPURequirements.Find(id);
            if (softwareCPURequirement != null)
                db.SoftwareCPURequirements.Remove(softwareCPURequirement);
        }
    }
}
