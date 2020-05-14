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
    class ComputerAssemblyHDDRepository: IRepository<ComputerAssemblyHDD>
    {
        private ApplicationContext db;

        public ComputerAssemblyHDDRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssemblyHDD> GetAll()
        {
            return db.ComputerAssemblyHDDs;
        }

        public ComputerAssemblyHDD Get(int id)
        {
            return db.ComputerAssemblyHDDs.Find(id);
        }

        public void Create(ComputerAssemblyHDD computerAssemblyHDD)
        {
            db.ComputerAssemblyHDDs.Add(computerAssemblyHDD);
        }

        public void Update(ComputerAssemblyHDD computerAssemblyHDD)
        {
            db.Entry(computerAssemblyHDD).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssemblyHDD> Find(Func<ComputerAssemblyHDD, Boolean> predicate)
        {
            return db.ComputerAssemblyHDDs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssemblyHDD computerAssemblyHDD = db.ComputerAssemblyHDDs.Find(id);
            if (computerAssemblyHDD != null)
                db.ComputerAssemblyHDDs.Remove(computerAssemblyHDD);
        }
    }
}
