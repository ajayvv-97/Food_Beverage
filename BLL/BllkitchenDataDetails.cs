
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllkitchenDataDetails<T> where T : class
    {
        public List<T> serachData(string Procedure, Dmnkitchendetails objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objSearch.OrderMasterId);
            parms.Add("OrderNo", objSearch.OrderNo);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
