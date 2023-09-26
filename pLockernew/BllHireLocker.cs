using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;

namespace BLL
{
    public class BllHireLocker
    {
        public bool AddEditData(string procedureName, DmnHireLocker objTestClass)
        {
            DAL.DapperRepository<DmnHireLocker> objDapperRepository = new DAL.DapperRepository<DmnHireLocker>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LockHireID", objTestClass.LockHireID);
            parms.Add("LockID", objTestClass.LockID);
            parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("AccID", objTestClass.AccID);
            parms.Add("AccNo", objTestClass.AccNo);
            parms.Add("AccPhone", objTestClass.AccPhone);
            parms.Add("AccEmail", objTestClass.AccEmail);
            parms.Add("CustomerPin", objTestClass.CustomerPin);
            parms.Add("HireFor", objTestClass.HireFor);
            parms.Add("RateType", objTestClass.RateType);
            parms.Add("Rate", objTestClass.Rate);
            parms.Add("Amt", objTestClass.Amt);
            parms.Add("ExpDate", objTestClass.ExpDate);
            parms.Add("ExpTime", objTestClass.ExpTime);
            parms.Add("HireDate", objTestClass.HireDate);
            parms.Add("HireTime", objTestClass.HireTime);
            parms.Add("LockOpen", objTestClass.LockOpen);
            parms.Add("Inotes", objTestClass.Inotes);
            parms.Add("Hiredetail", objTestClass.Hiredetail);
            parms.Add("HireNo", objTestClass.HireNo);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Hired", objTestClass.Hired);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;



        }
    }
}
