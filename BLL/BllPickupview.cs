
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllPickupview<T> where T : class
    {
        public List<T> SerachData(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Serachmode", objSearch.Field29);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}

