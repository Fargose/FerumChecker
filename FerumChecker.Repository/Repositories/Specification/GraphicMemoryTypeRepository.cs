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
    class GraphicMemoryTypeRepository : IRepository<GraphicMemoryType>
    {
        private ApplicationContext db;

        public GraphicMemoryTypeRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<GraphicMemoryType> GetAll()
        {
            return db.GraphicMemoryTypes;
        }

        public GraphicMemoryType Get(int id)
        {
            return db.GraphicMemoryTypes.Find(id);
        }

        public void Create(GraphicMemoryType graphicMemoryType)
        {
            db.GraphicMemoryTypes.Add(graphicMemoryType);
        }

        public void Update(GraphicMemoryType graphicMemoryType)
        {
            db.Entry(graphicMemoryType).State = EntityState.Modified;
        }
        public IEnumerable<GraphicMemoryType> Find(Func<GraphicMemoryType, Boolean> predicate)
        {
            return db.GraphicMemoryTypes.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            GraphicMemoryType graphicMemoryType = db.GraphicMemoryTypes.Find(id);
            if (graphicMemoryType != null)
                db.GraphicMemoryTypes.Remove(graphicMemoryType);
        }
    }
}
