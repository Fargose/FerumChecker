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
    class SoftwareVideoCardRequirementRepository: IRepository<SoftwareVideoCardRequirement>
    {
        private ApplicationContext db;

        public SoftwareVideoCardRequirementRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<SoftwareVideoCardRequirement> GetAll()
        {
            return db.SoftwareVideoCardRequirements;
        }

        public SoftwareVideoCardRequirement Get(int id)
        {
            return db.SoftwareVideoCardRequirements.Find(id);
        }

        public void Create(SoftwareVideoCardRequirement softwareVideoCardRequirement)
        {
            db.SoftwareVideoCardRequirements.Add(softwareVideoCardRequirement);
        }

        public void Update(SoftwareVideoCardRequirement softwareVideoCardRequirement)
        {
            db.Entry(softwareVideoCardRequirement).State = EntityState.Modified;
        }
        public IEnumerable<SoftwareVideoCardRequirement> Find(Func<SoftwareVideoCardRequirement, Boolean> predicate)
        {
            return db.SoftwareVideoCardRequirements.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            SoftwareVideoCardRequirement softwareVideoCardRequirement = db.SoftwareVideoCardRequirements.Find(id);
            if (softwareVideoCardRequirement != null)
                db.SoftwareVideoCardRequirements.Remove(softwareVideoCardRequirement);
        }
    }
}
