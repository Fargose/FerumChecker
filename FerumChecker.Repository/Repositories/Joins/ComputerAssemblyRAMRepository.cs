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
    class ComputerAssemblyRAMRepository: IRepository<ComputerAssemblyRAM>
    {
        private ApplicationContext db;

        public ComputerAssemblyRAMRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssemblyRAM> GetAll()
        {
            return db.ComputerAssemblyRAMs;
        }

        public ComputerAssemblyRAM Get(int id)
        {
            return db.ComputerAssemblyRAMs.Find(id);
        }

        public void Create(ComputerAssemblyRAM computerAssemblyRAM)
        {
            db.ComputerAssemblyRAMs.Add(computerAssemblyRAM);
        }

        public void Update(ComputerAssemblyRAM computerAssemblyRAM)
        {
            db.Entry(computerAssemblyRAM).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssemblyRAM> Find(Func<ComputerAssemblyRAM, Boolean> predicate)
        {
            return db.ComputerAssemblyRAMs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssemblyRAM computerAssemblyRAM = db.ComputerAssemblyRAMs.Find(id);
            if (computerAssemblyRAM != null)
                db.ComputerAssemblyRAMs.Remove(computerAssemblyRAM);
        }
    }
}
