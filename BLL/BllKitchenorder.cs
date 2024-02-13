
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllKitchenorder 
    {        
        public Dmnkitchen Kitchen_statusupdate(string procedureName, Dmnkitchen objTestClass)
        {
            DAL.DapperRepository<Dmnkitchen> objDapperRepository = new DAL.DapperRepository<Dmnkitchen>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Koid", objTestClass.Koid);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("KOTPrintStatus", objTestClass.KOTPrintStatus);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
