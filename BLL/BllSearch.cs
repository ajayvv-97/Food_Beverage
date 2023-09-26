using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllSearch<T> where T : class
    {
        public List<T> GetSearchData(string Procedure,DmnSearch objSearch)
        {
            
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Select", objSearch.Select);
            parms.Add("Table", objSearch.Table);
            parms.Add("Where", objSearch.WhereNdGroupBy);
            parms.Add("OrderBy", objSearch.OrderBy);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }
    }
}
