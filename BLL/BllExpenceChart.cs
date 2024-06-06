using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllExpenceChart<T> where T : class
    {
        public List<T> serachexpencedata(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("StartDate", objSearch.StartDate);
            parms.Add("EndDate", objSearch.EndDate);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
