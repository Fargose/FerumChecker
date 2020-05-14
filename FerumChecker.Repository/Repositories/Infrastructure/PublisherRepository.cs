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
    class PublisherRepository : IRepository<Publisher>
    {
        private ApplicationContext db;

        public PublisherRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return db.Publishers;
        }

        public Publisher Get(int id)
        {
            return db.Publishers.Find(id);
        }

        public void Create(Publisher publisher)
        {
            db.Publishers.Add(publisher);
        }

        public void Update(Publisher publisher)
        {
            db.Entry(publisher).State = EntityState.Modified;
        }
        public IEnumerable<Publisher> Find(Func<Publisher, Boolean> predicate)
        {
            return db.Publishers.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (publisher != null)
                db.Publishers.Remove(publisher);
        }
    }
}
