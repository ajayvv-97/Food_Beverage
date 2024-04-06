using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllAddon
    {
        public DmnAddon AddData(string procedureName, DmnAddon objTestClass)
        {
            DAL.DapperRepository<DmnAddon> objDapperRepository = new DAL.DapperRepository<DmnAddon>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Aid", objTestClass.Aid);
            parms.Add("AddonName", objTestClass.AddonName);
            parms.Add("Price", objTestClass.Price);
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Fcid", objTestClass.Fcid);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}