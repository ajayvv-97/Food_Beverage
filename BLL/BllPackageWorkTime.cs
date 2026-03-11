using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;

namespace BLL
{
    public class BllPackageWorkTime
    {
        public bool AddEditPackageWorkTime(string procedurename, DmnPackageWorkTime objTestClass)
        {
            DAL.DapperRepository<DmnPackageWorkTime> dapperRepository = new DAL.DapperRepository<DmnPackageWorkTime>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("PkgTimeId", objTestClass.PkgTimeId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("DayId", objTestClass.DayId);
            parms.Add("FromTime", objTestClass.FromTime);
            parms.Add("ToTime", objTestClass.ToTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("PartyPackId", objTestClass.PartyPackId);
            
            bool data = dapperRepository.Add(procedurename, parms);

            return data;
        }
    }
}