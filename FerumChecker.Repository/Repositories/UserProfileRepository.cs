using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories
{
    class UserProfileRepository: IRepository<UserProfile>
    {
        private ApplicationContext db;

        public UserProfileRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles;
        }

        public UserProfile Get(int id)
        {
            return db.UserProfiles.Find(id);
        }

        public void Create(UserProfile userProfile)
        {
            db.UserProfiles.Add(userProfile);
        }

        public void Update(UserProfile userProfile)
        {
            db.Entry(userProfile).State = EntityState.Modified;
        }
        public IEnumerable<UserProfile> Find(Func<UserProfile, Boolean> predicate)
        {
            return db.UserProfiles.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile != null)
                db.UserProfiles.Remove(userProfile);
        }
    }
}
