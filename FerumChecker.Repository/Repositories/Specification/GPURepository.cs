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
    public class GPURepository : IRepository<GPU>
    {
        private ApplicationContext db;

        public GPURepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<GPU> GetAll()
        {
            return db.GPUs;
        }

        public GPU Get(int id)
        {
            return db.GPUs.Find(id);
        }

        public void Create(GPU gpu)
        {
            db.GPUs.Add(gpu);
        }

        public void Update(GPU gpu)
        {
            db.Entry(gpu).State = EntityState.Modified;
        }
        public IEnumerable<GPU> Find(Func<GPU, Boolean> predicate)
        {
            return db.GPUs.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            GPU gpu = db.GPUs.Find(id);
            if (gpu != null)
                db.GPUs.Remove(gpu);
        }
    }
}
