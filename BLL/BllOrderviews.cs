using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllOrderviews<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SerachType", objSearch.SearchData);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}

