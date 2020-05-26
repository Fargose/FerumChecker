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
    class SSDRepository : IRepository<SSD>
    {
        private ApplicationContext db;

        public SSDRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<SSD> GetAll()
        {
            return db.SSDs.Include(m => m.OuterMemoryFormFactor).Include(m => m.OuterMemoryInterface).Include(m => m.Manufacturer);
        }

        public SSD Get(int id)
        {
            var ssd =  db.SSDs.Find(id);
            ssd.Manufacturer = db.Manufacturers.Find(ssd.ManufacturerId);
            ssd.OuterMemoryInterface = db.OuterMemoryInterfaces.Find(ssd.OuterMemoryInterfaceId);
            ssd.OuterMemoryFormFactor = db.OuterMemoryFormFactors.Find(ssd.OuterMemoryFormFactorId);
            return ssd;
        }

        public void Create(SSD ssd)
        {
            db.SSDs.Add(ssd);
        }

        public void Update(SSD ssd)
        {
            db.Entry(ssd).State = EntityState.Modified;
        }
        public IEnumerable<SSD> Find(Func<SSD, Boolean> predicate)
        {
            return db.SSDs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            SSD ssd = db.SSDs.Find(id);
            if (ssd != null)
                db.SSDs.Remove(ssd);
        }
    }
}
