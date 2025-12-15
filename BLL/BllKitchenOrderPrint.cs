
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllKitchenOrderPrint<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}

