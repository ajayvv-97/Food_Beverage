
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
            parms.Add("sFld2", objSearch.sFld2);
            parms.Add("sFld3", objSearch.sFld3);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }

    }
}
