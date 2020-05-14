using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories.User
{
    class ComputerAssemblyRateRepository: IRepository<ComputerAssemblyRate>
    {
        private ApplicationContext db;

        public ComputerAssemblyRateRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssemblyRate> GetAll()
        {
            return db.ComputerAssemblyRates;
        }

        public ComputerAssemblyRate Get(int id)
        {
            return db.ComputerAssemblyRates.Find(id);
        }

        public void Create(ComputerAssemblyRate computerAssemblyRate)
        {
            db.ComputerAssemblyRates.Add(computerAssemblyRate);
        }

        public void Update(ComputerAssemblyRate computerAssemblyRate)
        {
            db.Entry(computerAssemblyRate).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssemblyRate> Find(Func<ComputerAssemblyRate, Boolean> predicate)
        {
            return db.ComputerAssemblyRates.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssemblyRate computerAssemblyRate = db.ComputerAssemblyRates.Find(id);
            if (computerAssemblyRate != null)
                db.ComputerAssemblyRates.Remove(computerAssemblyRate);
        }
    }
}
