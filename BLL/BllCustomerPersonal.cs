using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllCustomerPersonal
    {
        public DmnCustomerPersonal AddData(string procedureName, DmnCustomerPersonal objTestClass)
        {
            DAL.DapperRepository<DmnCustomerPersonal> dapperRepository = new DAL.DapperRepository<DmnCustomerPersonal>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CustomerCode", objTestClass.CustomerCode);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("FirstName", objTestClass.FirstName);
            parms.Add("LastName", objTestClass.LastName);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("DOB", objTestClass.DOB);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("MaritalStatus", objTestClass.MaritalStatus);
            parms.Add("CustomerAddress", objTestClass.CustomerAddress);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;

        }
        public bool UpdateData(string procedureName, DmnCustomerPersonal objTestClass)
        {
            DAL.DapperRepository<DmnCustomerPersonal> dapperRepository = new DAL.DapperRepository<DmnCustomerPersonal>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CustomerId", objTestClass.CustomerId);
            parms.Add("CustomerCode", objTestClass.CustomerCode);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("FirstName", objTestClass.FirstName);
            parms.Add("LastName", objTestClass.LastName);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("DOB", objTestClass.DOB);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("MaritalStatus", objTestClass.MaritalStatus);
            parms.Add("CustomerAddress", objTestClass.CustomerAddress);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = dapperRepository.Update(procedureName, parms);
            return lstData;

        }
        public bool DeleteById(string procedureName, DmnCustomerPersonal objTestClass)
        {
            DAL.DapperRepository<DmnCustomerPersonal> dapperRepository = new DAL.DapperRepository<DmnCustomerPersonal>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CustomerId", objTestClass.CustomerId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = dapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
