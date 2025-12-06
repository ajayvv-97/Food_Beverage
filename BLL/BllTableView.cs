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
            parms.Add("serachBytablcodeStatus", objSearch.Field26);
            parms.Add("Tablcode", objSearch.Field27);
            parms.Add("serachBycategoryStatus", objSearch.Field25);
            parms.Add("Pagecount", objSearch.Field24); 
             var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}

