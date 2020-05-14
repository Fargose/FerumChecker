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
    class ComputerAssemblySSDRepository: IRepository<ComputerAssemblySSD>
    {
        private ApplicationContext db;

        public ComputerAssemblySSDRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssemblySSD> GetAll()
        {
            return db.ComputerAssemblySSDs;
        }

        public ComputerAssemblySSD Get(int id)
        {
            return db.ComputerAssemblySSDs.Find(id);
        }

        public void Create(ComputerAssemblySSD computerAssemblySSD)
        {
            db.ComputerAssemblySSDs.Add(computerAssemblySSD);
        }

        public void Update(ComputerAssemblySSD computerAssemblySSD)
        {
            db.Entry(computerAssemblySSD).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssemblySSD> Find(Func<ComputerAssemblySSD, Boolean> predicate)
        {
            return db.ComputerAssemblySSDs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssemblySSD computerAssemblySSD = db.ComputerAssemblySSDs.Find(id);
            if (computerAssemblySSD != null)
                db.ComputerAssemblySSDs.Remove(computerAssemblySSD);
        }
    }
}
