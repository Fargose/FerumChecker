using FerumChecker.DataAccess.Entities;
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
    class VideoCardRepository : IRepository<VideoCard>
    {
        private ApplicationContext db;

        public VideoCardRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<VideoCard> GetAll()
        {
            return db.VideoCards;
        }

        public VideoCard Get(int id)
        {
            return db.VideoCards.Find(id);
        }

        public void Create(VideoCard videoCard)
        {
            db.VideoCards.Add(videoCard);
        }

        public void Update(VideoCard videoCard)
        {
            db.Entry(videoCard).State = EntityState.Modified;
        }
        public IEnumerable<VideoCard> Find(Func<VideoCard, Boolean> predicate)
        {
            return db.VideoCards.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            VideoCard videoCard = db.VideoCards.Find(id);
            if (videoCard != null)
                db.VideoCards.Remove(videoCard);
        }
    }
}
