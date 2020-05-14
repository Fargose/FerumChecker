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
    class ManufacturerRepository : IRepository<Manufacturer>
    {
        private ApplicationContext db;

        public ManufacturerRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return db.Manufacturers;
        }

        public Manufacturer Get(int id)
        {
            return db.Manufacturers.Find(id);
        }

        public void Create(Manufacturer manufacturer)
        {
            db.Manufacturers.Add(manufacturer);
        }

        public void Update(Manufacturer manufacturer)
        {
            db.Entry(manufacturer).State = EntityState.Modified;
        }
        public IEnumerable<Manufacturer> Find(Func<Manufacturer, Boolean> predicate)
        {
            return db.Manufacturers.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer != null)
                db.Manufacturers.Remove(manufacturer);
        }
    }
}
