
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllAnnuvalreport<T> where T : class
    {
        public List<T> serachreport(string Procedure, DmnRpt_1 objSearch)
        {

            DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("dt1", objSearch.dt1);
            parms.Add("dt2", objSearch.dt2);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
