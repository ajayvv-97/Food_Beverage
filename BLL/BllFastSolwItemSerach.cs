
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
namespace BLL
{
    public class BllFastSolwItemSerach<T> where T : class
    {
        public List<T> serachSolwItemSerach(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("StartDate", objSearch.Field1);
            parms.Add("EndDate", objSearch.Field2);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
