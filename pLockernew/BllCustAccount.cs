using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;

namespace BLL
{
    public class BllCustAccount
    {
        public bool AddEditData(string procedureName, DmnCustAccount objTestClass)
        {
            DAL.DapperRepository<DmnCustAccount> objDapperRepository = new DAL.DapperRepository<DmnCustAccount>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("AccID", objTestClass.AccID);
            parms.Add("AccNo", objTestClass.Gender);
            parms.Add("AccName", objTestClass.AccNo);
            parms.Add("AccPhone", objTestClass.AccName);
            parms.Add("AccEmail", objTestClass.AccPhone);
            parms.Add("AccAddr", objTestClass.AccEmail);
            parms.Add("Gender", objTestClass.AccAddress);
            parms.Add("Proof", objTestClass.Proof);
            parms.Add("ProofNo", objTestClass.ProofNo);
            parms.Add("DOB", objTestClass.DOB);
            parms.Add("Age", objTestClass.Age);
            parms.Add("Remark ", objTestClass.Age);

            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;



        }
    }
}
