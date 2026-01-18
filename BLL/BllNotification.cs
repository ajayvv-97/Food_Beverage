using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{

    public class BllNotification<T> where T : class
    {
        public List<T> SerachOrder(string Procedure, DmnGridFields objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@BranchId", objSearch.Field30);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
