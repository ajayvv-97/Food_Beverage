using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllKitchenData<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Serachdata", objSearch.Field30);
            parms.Add("Serachtype", objSearch.Searchtype);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}

