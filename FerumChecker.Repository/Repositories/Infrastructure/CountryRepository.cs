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
    class CountryRepository : IRepository<Country>
    {
        private ApplicationContext db;

        public CountryRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Country> GetAll()
        {
            return db.Countries;
        }

        public Country Get(int id)
        {
            return db.Countries.Find(id);
        }

        public void Create(Country software)
        {
            db.Countries.Add(software);
        }

        public void Update(Country software)
        {
            db.Entry(software).State = EntityState.Modified;
        }
        public IEnumerable<Country> Find(Func<Country, Boolean> predicate)
        {
            return db.Countries.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Country software = db.Countries.Find(id);
            if (software != null)
                db.Countries.Remove(software);
        }
    }
}
