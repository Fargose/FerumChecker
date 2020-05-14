using FerumChecker.DataAccess.Entities.Joins;
using FerumChecker.DataAccess.Entities.User;
using FerumChecker.Repository.EF;
using FerumChecker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FerumChecker.Repository.Repositories.Joins
{
    class ComputerAssemblyVideoCardRepository: IRepository<ComputerAssemblyVideoCard>
    {
        private ApplicationContext db;

        public ComputerAssemblyVideoCardRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<ComputerAssemblyVideoCard> GetAll()
        {
            return db.ComputerAssemblyVideoCards;
        }

        public ComputerAssemblyVideoCard Get(int id)
        {
            return db.ComputerAssemblyVideoCards.Find(id);
        }

        public void Create(ComputerAssemblyVideoCard computerAssemblyVideoCard)
        {
            db.ComputerAssemblyVideoCards.Add(computerAssemblyVideoCard);
        }

        public void Update(ComputerAssemblyVideoCard computerAssemblyVideoCard)
        {
            db.Entry(computerAssemblyVideoCard).State = EntityState.Modified;
        }
        public IEnumerable<ComputerAssemblyVideoCard> Find(Func<ComputerAssemblyVideoCard, Boolean> predicate)
        {
            return db.ComputerAssemblyVideoCards.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            ComputerAssemblyVideoCard computerAssemblyVideoCard = db.ComputerAssemblyVideoCards.Find(id);
            if (computerAssemblyVideoCard != null)
                db.ComputerAssemblyVideoCards.Remove(computerAssemblyVideoCard);
        }
    }
}
