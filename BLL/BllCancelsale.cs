using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{

    public class BllCancelsale<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmnOrderCancel objSearch)
        {
            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
