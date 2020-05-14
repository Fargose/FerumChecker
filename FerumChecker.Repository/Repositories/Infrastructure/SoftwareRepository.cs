﻿using FerumChecker.DataAccess.Entities;
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
    class SoftwareRepository : IRepository<Software>
    {
        private ApplicationContext db;

        public SoftwareRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Software> GetAll()
        {
            return db.Softwares;
        }

        public Software Get(int id)
        {
            return db.Softwares.Find(id);
        }

        public void Create(Software software)
        {
            db.Softwares.Add(software);
        }

        public void Update(Software software)
        {
            db.Entry(software).State = EntityState.Modified;
        }
        public IEnumerable<Software> Find(Func<Software, Boolean> predicate)
        {
            return db.Softwares.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Software software = db.Softwares.Find(id);
            if (software != null)
                db.Softwares.Remove(software);
        }
    }
}
