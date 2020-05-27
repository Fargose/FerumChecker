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
    class ComputerAssemblyRepository : IRepository<ComputerAssembly>
    {
        private ApplicationContext db;

        public ComputerAssemblyRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssembly> GetAll()
        {
            return db.ComputerAssemblies.Include(m => m.CPU)
                .Include(m => m.MotherBoard)
                .Include(m => m.PowerSupply)
                .Include(m => m.PCCase)
                .Include(m => m.VideoCards).ThenInclude(m => m.VideoCard)
                .Include(m => m.ComputerAssemblyRAMs).ThenInclude(m => m.RAM)
                .Include(m => m.HDDs).ThenInclude(m => m.HDD)
                .Include(m => m.SSDs).ThenInclude(m => m.SSD)
                .Include(m => m.Owner);
        }

        public ComputerAssembly Get(int id)
        {
            return db.ComputerAssemblies
                .Include(m => m.CPU)
                .Include(m => m.MotherBoard)
                .Include(m => m.PowerSupply)
                .Include(m => m.PCCase)
                .Include(m => m.VideoCards).ThenInclude(m => m.VideoCard)
                .Include(m => m.ComputerAssemblyRAMs).ThenInclude(m => m.RAM)
                .Include(m => m.HDDs).ThenInclude(m => m.HDD)
                .Include(m => m.SSDs).ThenInclude(m => m.SSD)
                .Include(m => m.Owner)
                .FirstOrDefault(m => m.Id == id);
        }

        public void Create(ComputerAssembly computerAssembly)
        {
            db.ComputerAssemblies.Add(computerAssembly);
        }

        public void Update(ComputerAssembly computerAssembly)
        {
            db.Entry(computerAssembly).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssembly> Find(Func<ComputerAssembly, Boolean> predicate)
        {
            return db.ComputerAssemblies.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssembly computerAssembly = db.ComputerAssemblies.Find(id);
            if (computerAssembly != null)
                db.ComputerAssemblies.Remove(computerAssembly);
        }
    }
}
