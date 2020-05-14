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
    public class OuterMemoryInterfaceRepository : IRepository<OuterMemoryInterface>
    {
        private ApplicationContext db;

        public OuterMemoryInterfaceRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<OuterMemoryInterface> GetAll()
        {
            return db.OuterMemoryInterfaces;
        }

        public OuterMemoryInterface Get(int id)
        {
            return db.OuterMemoryInterfaces.Find(id);
        }

        public void Create(OuterMemoryInterface outerMemoryInterface)
        {
            db.OuterMemoryInterfaces.Add(outerMemoryInterface);
        }

        public void Update(OuterMemoryInterface outerMemoryInterface)
        {
            db.Entry(outerMemoryInterface).State = EntityState.Modified;
        }
        public IEnumerable<OuterMemoryInterface> Find(Func<OuterMemoryInterface, Boolean> predicate)
        {
            return db.OuterMemoryInterfaces.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            OuterMemoryInterface outerMemoryInterface = db.OuterMemoryInterfaces.Find(id);
            if (outerMemoryInterface != null)
                db.OuterMemoryInterfaces.Remove(outerMemoryInterface);
        }
    }
}
