using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllCompany
    {

        public bool AddData(string procedureName, DmnCompany objTestClass)
        {//
            DAL.DapperRepository<DmnCompany> objDapperRepository = new DAL.DapperRepository<DmnCompany>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CompanyCode", objTestClass.CompanyCode);
            parms.Add("CompanyName", objTestClass.CompanyName);
            parms.Add("CompanyAddress", objTestClass.CompanyAddress);
            parms.Add("PostalCode", objTestClass.PostalCode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Fax", objTestClass.Fax);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Website", objTestClass.Website);
            parms.Add("CompanyBankId", objTestClass.CompanyBankId);
            parms.Add("PAN", objTestClass.PAN);
            parms.Add("ContactName", objTestClass.ContactName);
            parms.Add("ContactNo", objTestClass.ContactNo);
            parms.Add("Logo", objTestClass.Logo);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("TINNo", objTestClass.TINNo);
            parms.Add("STaxNo", objTestClass.STaxNo);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //  parms.Add("Status", objTestClass.Status);
            var saveStatus = objDapperRepository.Add(procedureName, parms);
            return saveStatus;
        }

        public DmnCompany getDataById(string procedureName, DmnCompany objTestClass)
        {
            DAL.DapperRepository<DmnCompany> objDapperRepository = new DAL.DapperRepository<DmnCompany>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CompanyId", objTestClass.CompanyId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnCompany objTestClass)
        {
            DAL.DapperRepository<DmnCompany> objDapperRepository = new DAL.DapperRepository<DmnCompany>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CompanyId", objTestClass.CompanyId);
            parms.Add("CompanyCode", objTestClass.CompanyCode);
            parms.Add("CompanyName", objTestClass.CompanyName);
            parms.Add("CompanyAddress", objTestClass.CompanyAddress);
            parms.Add("PostalCode", objTestClass.PostalCode);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Fax", objTestClass.Fax);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Website", objTestClass.Website);
            parms.Add("CompanyBankId", objTestClass.CompanyBankId);
            parms.Add("PAN", objTestClass.PAN);
            parms.Add("ContactName", objTestClass.ContactName);
            parms.Add("ContactNo", objTestClass.ContactNo);
            parms.Add("Logo", objTestClass.Logo);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("TINNo", objTestClass.TINNo);
            parms.Add("STaxNo", objTestClass.STaxNo);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnCompany objTestClass)
        {
            DAL.DapperRepository<DmnCompany> objDapperRepository = new DAL.DapperRepository<DmnCompany>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CompanyId", objTestClass.CompanyId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
