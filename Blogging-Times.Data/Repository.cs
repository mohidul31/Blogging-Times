using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace Blogging_Times.Data
{
    public class Repository<T> : IRepository<T> where T:Entity
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

        public IEnumerable<T> GetAjaxDatatablePagedDataList(
            DataTablesAjaxRequestModel datatableRequest,
            out int recordsTotal,
            out int recordsFiltered,
            string[] tableColumnmList = null,
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null,
            bool isTrackingOff = false)
        {

            // Datatable Request
            int start = datatableRequest.GetStart();
            int length = datatableRequest.GetLength();
            string sortColumnName = datatableRequest.GetSortColumnName(tableColumnmList);
            string sortDirection = datatableRequest.GetSortDirection();

            //Initial return Results
            recordsTotal = 0;
            recordsFiltered = 0;
            IQueryable<T> result = _db.Set<T>();
            recordsTotal = result.Count();
            length = (length == -1 ? recordsTotal : length);

            //Include
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    result = result.Include(includeProperty);
                }
            }

            //Filter
            if (filter != null)
            {
                result = result.Where(filter);
            }
            recordsFiltered = result.Count();

            //Sorting
            if (sortColumnName != null && sortDirection != null)
            {
                result = result.OrderBy(sortColumnName + " " + sortDirection);
            }
            else
            {
                //Default Order
                result = result.OrderByDescending(o => o.CreatedAt);
            }

            //Paging
            result = result.Skip(start).Take(length);

            if (isTrackingOff)
            {
                result = result.AsNoTracking();
            }
            //return
            return result.ToList();
        }
    }
}
