using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllAccountLedger
    {
        public DmnAccountLedger AddData(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerCode", objTestClass.LedgerCode);
            parms.Add("LedgerName", objTestClass.LedgerName);
            parms.Add("AccountGroupId", objTestClass.AccountGroupId);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("LedgerAddress", objTestClass.LedgerAddress);
            parms.Add("TINNo", objTestClass.TINNo);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("CreditLimit", objTestClass.CreditLimit);
            parms.Add("CreditDays", objTestClass.CreditDays); 
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("InterestPerDay", objTestClass.InterestPerDay);
            parms.Add("BankId", objTestClass.BankId);
            parms.Add("BankAccountNo", objTestClass.BankAccountNo);
            parms.Add("BankBranch", objTestClass.BankBranch);
            parms.Add("BankIFSC", objTestClass.BankIFSC);
            parms.Add("PAN", objTestClass.PAN);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("CompanyRegNo", objTestClass.CompanyRegNo);
            parms.Add("State", objTestClass.State);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("Cardtype", objTestClass.cardtype);
            parms.Add("vfrom", objTestClass.vfrom);
            parms.Add("vto ", objTestClass.vto );
            parms.Add("cardstatus", objTestClass.cardstatus);
            parms.Add("Age", objTestClass.age);
            parms.Add("Gender", objTestClass.gender);
            parms.Add("dob", objTestClass.dob);


            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;

            return lstData;
        }
        public DmnAccountLedger getDataById(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerId", objTestClass.LedgerId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("LedgerCode", objTestClass.LedgerCode);
            parms.Add("LedgerName", objTestClass.LedgerName);
            parms.Add("AccountGroupId", objTestClass.AccountGroupId);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("LedgerAddress", objTestClass.LedgerAddress);
            parms.Add("TINNo", objTestClass.TINNo);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("CreditLimit", objTestClass.CreditLimit);
            parms.Add("CreditDays", objTestClass.CreditDays);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("InterestPerDay", objTestClass.InterestPerDay);
            parms.Add("BankId", objTestClass.BankId);
            parms.Add("BankAccountNo", objTestClass.BankAccountNo);
            parms.Add("BankBranch", objTestClass.BankBranch);
            parms.Add("BankIFSC", objTestClass.BankIFSC);
            parms.Add("PAN", objTestClass.PAN);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("CompanyRegNo", objTestClass.CompanyRegNo);
            parms.Add("State", objTestClass.State);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("Cardtype", objTestClass.cardtype);
            parms.Add("vfrom", objTestClass.vfrom);
            parms.Add("vto ", objTestClass.vto);
            parms.Add("cardstatus", objTestClass.cardstatus);
            parms.Add("Age", objTestClass.age);
            parms.Add("Gender", objTestClass.gender);
            parms.Add("dob", objTestClass.dob);

            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("LedgerId", objTestClass.LedgerId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool UpdateLedgerAddress_Mobile(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("LedgerAddress", objTestClass.LedgerAddress);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool UpdateTotalLoyalty(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("TotLoyPnts", objTestClass.Total_loypoints);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

        public bool UpdateREDIM_Value(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("RedimPnts", objTestClass.redimpoints);
            parms.Add("LoyPnts", objTestClass.loypoints);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

        public bool UpdateCustomerLoyalty(string procedureName, DmnAccountLedger objTestClass)
        {
            DAL.DapperRepository<DmnAccountLedger> objDapperRepository = new DAL.DapperRepository<DmnAccountLedger>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);

            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}
