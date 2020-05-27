using System;
using System.Collections.Generic;
using System.Text;

namespace FerumChecker.Repository.Interfaces
{
    public interface IStringRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
