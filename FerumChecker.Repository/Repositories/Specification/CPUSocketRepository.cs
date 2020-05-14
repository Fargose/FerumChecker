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
    class CPUSocketRepository : IRepository<CPUSocket>
    {
        private ApplicationContext db;

        public CPUSocketRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<CPUSocket> GetAll()
        {
            return db.CPUSockets;
        }

        public CPUSocket Get(int id)
        {
            return db.CPUSockets.Find(id);
        }

        public void Create(CPUSocket cpuSocket)
        {
            db.CPUSockets.Add(cpuSocket);
        }

        public void Update(CPUSocket cpuSocket)
        {
            db.Entry(cpuSocket).State = EntityState.Modified;
        }
        public IEnumerable<CPUSocket> Find(Func<CPUSocket, Boolean> predicate)
        {
            return db.CPUSockets.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            CPUSocket cpuSocket = db.CPUSockets.Find(id);
            if (cpuSocket != null)
                db.CPUSockets.Remove(cpuSocket);
        }
    }
}
