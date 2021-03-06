﻿using FerumChecker.DataAccess.Entities;
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
    class CPURepository : IRepository<CPU>
    {
        private ApplicationContext db;

        public CPURepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<CPU> GetAll()
        {
            return db.CPUs.Include(m => m.CPUSocket).Include(m => m.Manufacturer);
        }

        public CPU Get(int id)
        {
            var cpu  = db.CPUs.Find(id);
            cpu.CPUSocket = db.CPUSockets.Find(cpu.CPUSocketId);
            cpu.Manufacturer = db.Manufacturers.Find(cpu.ManufacturerId);
            return cpu;
        }

        public void Create(CPU cpu)
        {
            db.CPUs.Add(cpu);
        }

        public void Update(CPU cpu)
        {
            db.Entry(cpu).State = EntityState.Modified;
        }
        public IEnumerable<CPU> Find(Func<CPU, Boolean> predicate)
        {
            return db.CPUs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            CPU cpu = db.CPUs.Find(id);
            if (cpu != null)
                db.CPUs.Remove(cpu);
        }
    }
}
