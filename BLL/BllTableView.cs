using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllTableView<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BookingInterwell", objSearch.Field30);
            parms.Add("Serachmode", objSearch.Field29);
            parms.Add("categoryid", objSearch.Field28); 
             var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}

