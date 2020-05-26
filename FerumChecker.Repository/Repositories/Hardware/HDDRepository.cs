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
    class HDDRepository : IRepository<HDD>
    {
        private ApplicationContext db;

        public HDDRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<HDD> GetAll()
        {
            return db.HDDs.Include(m => m.OuterMemoryFormFactor).Include(m => m.OuterMemoryInterface).Include(m => m.Manufacturer);
        }

        public HDD Get(int id)
        {
            var hdd =  db.HDDs.Find(id);
            hdd.Manufacturer = db.Manufacturers.Find(hdd.ManufacturerId);
            hdd.OuterMemoryInterface = db.OuterMemoryInterfaces.Find(hdd.OuterMemoryInterfaceId);
            hdd.OuterMemoryFormFactor = db.OuterMemoryFormFactors.Find(hdd.OuterMemoryFormFactorId);
            return hdd;
        }

        public void Create(HDD hdd)
        {
            db.HDDs.Add(hdd);
        }

        public void Update(HDD hdd)
        {
            db.Entry(hdd).State = EntityState.Modified;
        }
        public IEnumerable<HDD> Find(Func<HDD, Boolean> predicate)
        {
            return db.HDDs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            HDD hdd = db.HDDs.Find(id);
            if (hdd != null)
                db.HDDs.Remove(hdd);
        }
    }
}
