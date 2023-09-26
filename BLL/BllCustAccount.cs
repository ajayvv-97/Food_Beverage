using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;

namespace BLL
{
    public class BllCustAccount
    {
        public DmnAccountLedger AddEditData(string procedureName, DmnCustAccount objTestClass)
        {         
            DAL.DapperRepository<DmnCustAccount> objDapperRepository = new DAL.DapperRepository<DmnCustAccount>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("AccID", objTestClass.AccID);
            parms.Add("AccNo", objTestClass.AccNo);
            parms.Add("AccName", objTestClass.AccName);
            parms.Add("AccPhone", objTestClass.AccPhone);
            parms.Add("AccEmail", objTestClass.AccEmail);
            parms.Add("AccAddress", objTestClass.AccAddress);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("Proof", objTestClass.Proof);
            parms.Add("ProofNo", objTestClass.ProofNo);
            parms.Add("DOB", objTestClass.DOB);
            parms.Add("Age", objTestClass.Age);
            parms.Add("Remark", objTestClass.Remarks);
            parms.Add("EnrollDate", objTestClass.EnrollDate);
            parms.Add("ZipCode", objTestClass.ZipCode);
            parms.Add("Proofimg1", objTestClass.Proofimg1);
            parms.Add("Occupation", objTestClass.Occupation);
            parms.Add("CInstitute ", objTestClass.CInstitute);
            parms.Add("AllowEmail", objTestClass.AllowEmail);
            parms.Add("AllowSMS", objTestClass.AllowSMS);
            parms.Add("cCountry", objTestClass.cCountry);
            parms.Add("cEmergency", objTestClass.cEmergency);
            parms.Add("State", objTestClass.State);
            parms.Add("City", objTestClass.City);
            parms.Add("CardType", objTestClass.cardtype);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            //parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("MemberShip", objTestClass.MemberShip);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;



        }
    }
}
