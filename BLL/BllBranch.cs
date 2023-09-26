using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllBranch
    {
        public bool AddData(string procedureName, DmnBranch objTestClass)
        {
            DAL.DapperRepository<DmnBranch> objDapperRepository = new DAL.DapperRepository<DmnBranch>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CompanyId", objTestClass.CompanyId);
            parms.Add("BranchCode", objTestClass.BranchCode);
            parms.Add("BranchName", objTestClass.BranchName);
            parms.Add("BranchAddress", objTestClass.BranchAddress);
            parms.Add("PostalCode", objTestClass.PostalCode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Fax", objTestClass.Fax);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Website", objTestClass.Website);
            parms.Add("Logo", objTestClass.Logo);
            parms.Add("CompanyBankId", objTestClass.CompanyBankId);
            parms.Add("PAN", objTestClass.PAN);
            parms.Add("ContactName", objTestClass.ContactName);
            parms.Add("ContactNo", objTestClass.ContactNo);
           // parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("TINNo", objTestClass.TINNo);
            parms.Add("SalesPrefix", objTestClass.SalesPrefix);
            parms.Add("PurchaseSrlNo", objTestClass.PurchaseSrlNo);
            parms.Add("SalesSrlNo", objTestClass.SalesSrlNo);
            parms.Add("PurchaseReturnSrlNo", objTestClass.PurchaseReturnSrlNo);
            parms.Add("SalesReturnSrlNo", objTestClass.SalesReturnSrlNo);
            parms.Add("InwardPrefix", objTestClass.InwardPrefix);
            parms.Add("InwardSrlNo", objTestClass.InwardSrlNo);
            parms.Add("OutwardPrefix", objTestClass.OutwardPrefix);
            parms.Add("OutwardSrlNo", objTestClass.OutwardSrlNo);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
          //  parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }        
        public DmnBranch getDataById(string procedureName, DmnBranch objTestClass)
        {
            DAL.DapperRepository<DmnBranch> objDapperRepository = new DAL.DapperRepository<DmnBranch>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnBranch objTestClass)
        {
            DAL.DapperRepository<DmnBranch> objDapperRepository = new DAL.DapperRepository<DmnBranch>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CompanyId", objTestClass.CompanyId);
            parms.Add("BranchCode", objTestClass.BranchCode);
            parms.Add("BranchName", objTestClass.BranchName);
            parms.Add("BranchAddress", objTestClass.BranchAddress);
            parms.Add("PostalCode", objTestClass.PostalCode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Fax", objTestClass.Fax);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Website", objTestClass.Website);
            parms.Add("Logo", objTestClass.Logo);
            parms.Add("CompanyBankId", objTestClass.CompanyBankId);
            parms.Add("PAN", objTestClass.PAN);
            parms.Add("ContactName", objTestClass.ContactName);
            parms.Add("ContactNo", objTestClass.ContactNo);
          //  parms.Add("FinancialYearId", objTestClass.FinancialYearId);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("TINNo", objTestClass.TINNo);
            parms.Add("SalesPrefix", objTestClass.SalesPrefix);
            parms.Add("SalesSrlNo", objTestClass.SalesSrlNo);
            parms.Add("PurchaseSrlNo", objTestClass.PurchaseSrlNo);
            parms.Add("PurchaseReturnSrlNo", objTestClass.PurchaseReturnSrlNo);
            parms.Add("SalesReturnSrlNo", objTestClass.SalesReturnSrlNo);
            parms.Add("InwardPrefix", objTestClass.InwardPrefix);
            parms.Add("InwardSrlNo", objTestClass.InwardSrlNo);
            parms.Add("OutwardPrefix", objTestClass.OutwardPrefix);
            parms.Add("OutwardSrlNo", objTestClass.OutwardSrlNo);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
           // parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
          //  parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnBranch objTestClass)
        {
            DAL.DapperRepository<DmnBranch> objDapperRepository = new DAL.DapperRepository<DmnBranch>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
