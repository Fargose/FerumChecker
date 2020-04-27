using FerumChecker.DataAccess.Entities;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories
{
    class ComputerAssemblyRepository : IRepository<ComputerAssembly>
    {
        private ApplicationContext db;

        public ComputerAssemblyRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssembly> GetAll()
        {
            return db.ComputerAssemblies;
        }

        public ComputerAssembly Get(int id)
        {
            return db.ComputerAssemblies.Find(id);
        }

        public void Create(ComputerAssembly order)
        {
            db.ComputerAssemblies.Add(order);
        }

        public void Update(ComputerAssembly order)
        {
            db.Entry(order).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssembly> Find(Func<ComputerAssembly, Boolean> predicate)
        {
            return db.ComputerAssemblies.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssembly order = db.ComputerAssemblies.Find(id);
            if (order != null)
                db.ComputerAssemblies.Remove(order);
        }
    }
}
