using FerumChecker.DataAccess.Entities;
using FerumChecker.DataAccess.Entities.Infrastructure;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories.Infrastructure
{
    class DeveloperRepository : IRepository<Developer>
    {
        private ApplicationContext db;

        public DeveloperRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Developer> GetAll()
        {
            return db.Developers;
        }

        public Developer Get(int id)
        {
            return db.Developers.Find(id);
        }

        public void Create(Developer developer)
        {
            db.Developers.Add(developer);
        }

        public void Update(Developer developer)
        {
            db.Entry(developer).State = EntityState.Modified;
        }
        public IEnumerable<Developer> Find(Func<Developer, Boolean> predicate)
        {
            return db.Developers.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Developer developer = db.Developers.Find(id);
            if (developer != null)
                db.Developers.Remove(developer);
        }
    }
}
