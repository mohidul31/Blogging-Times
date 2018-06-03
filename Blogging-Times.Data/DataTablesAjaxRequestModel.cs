using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blogging_Times.Data
{
    public class DataTablesAjaxRequestModel
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public string GetSearchText()
        {
            string searchText = HttpContext.Current.Request["search[value]"];
            return searchText;
        }

        public string GetSortDirection()
        {
            string sortDirection = HttpContext.Current.Request["order[0][dir]"];
            return sortDirection;
        }

        public string GetSortColumnName(string[] columnOrder)
        {
            string sortColumnName = columnOrder[Convert.ToInt32(HttpContext.Current.Request["order[0][column]"])];
            return sortColumnName;
        }
        public int GetStart()
        {
            return (Start > 0 ? Start : 0);
        }

        public int GetLength()
        {
            return (Length == 0 ? 10 : Length);
        }

        public int GetSerialNoOfFirstRow()
        {
            return GetStart() + 1;
        }
    }
}
