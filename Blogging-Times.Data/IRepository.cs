using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blogging_Times.Data
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T item);
        void DeleteByID(Guid id);
        void DeleteByItem(T item);
        IEnumerable<T> GetAjaxDatatablePagedDataList(DataTablesAjaxRequestModel datatableRequest, out int recordsTotal, out int recordsFiltered, string[] tableColumnmList = null, Expression<Func<T, bool>> filter = null, string includeProperties = null, bool isTrackingOff = false);
        IEnumerable<T> GetAll();
        T GetByID(Guid id);
    }
}