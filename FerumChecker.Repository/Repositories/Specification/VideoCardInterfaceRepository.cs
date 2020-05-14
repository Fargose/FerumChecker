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
    public class VideoCardInterfaceRepository : IRepository<VideoCardInterface>
    {
        private ApplicationContext db;

        public VideoCardInterfaceRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<VideoCardInterface> GetAll()
        {
            return db.VideoCardInterfaces;
        }

        public VideoCardInterface Get(int id)
        {
            return db.VideoCardInterfaces.Find(id);
        }

        public void Create(VideoCardInterface videoCardInterface)
        {
            db.VideoCardInterfaces.Add(videoCardInterface);
        }

        public void Update(VideoCardInterface videoCardInterface)
        {
            db.Entry(videoCardInterface).State = EntityState.Modified;
        }
        public IEnumerable<VideoCardInterface> Find(Func<VideoCardInterface, Boolean> predicate)
        {
            return db.VideoCardInterfaces.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            VideoCardInterface videoCardInterface = db.VideoCardInterfaces.Find(id);
            if (videoCardInterface != null)
                db.VideoCardInterfaces.Remove(videoCardInterface);
        }
    }
}
