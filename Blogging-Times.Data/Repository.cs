using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class Repository<T> where T:Entity
    {
        private DbContext _db;
        public Repository(DbContext context)
        {
            _db = context;
        }

        public void Add(T item)
        {
            _db.Set<T>().Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetByID(Guid id)
        {
            return _db.Set<T>().Where(x => x.ID == id).SingleOrDefault();
        }

        public void DeleteByID(Guid id)
        {
            T item = GetByID(id);
            DeleteByItem(item);
        }

        public void DeleteByItem(T item)
        {
            _db.Set<T>().Remove(item);
        }
    }
}
