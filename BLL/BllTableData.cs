
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllTableData<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmncombinedBill objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BookingInterwell", objSearch.BookingInterwell);
            parms.Add("Fcid", objSearch.Fcid);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
