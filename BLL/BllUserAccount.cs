using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllUserAccount
    {
        public DmnUserAccount AddData(string procedureName, DmnUserAccount objTestClass)
        {
            DAL.DapperRepository<DmnUserAccount> objDapperRepository = new DAL.DapperRepository<DmnUserAccount>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountId", 0);
            parms.Add("UserAccountTypeId", objTestClass.UserAccountTypeId);
            parms.Add("Username", objTestClass.Username);
            parms.Add("PasswordHash", objTestClass.PasswordHash);
            parms.Add("PasswordSalt", objTestClass.PasswordSalt);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnUserAccount getDataById(string procedureName, DmnUserAccount objTestClass)
        {
            DAL.DapperRepository<DmnUserAccount> objDapperRepository = new DAL.DapperRepository<DmnUserAccount>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountId", objTestClass.UserAccountId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnUserAccount objTestClass)
        {
            DAL.DapperRepository<DmnUserAccount> objDapperRepository = new DAL.DapperRepository<DmnUserAccount>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountId", objTestClass.UserAccountId);
            parms.Add("UserAccountTypeId", objTestClass.UserAccountTypeId);
            parms.Add("Username", objTestClass.Username);
            parms.Add("PasswordHash", objTestClass.PasswordHash);
            parms.Add("PasswordSalt", objTestClass.PasswordSalt);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);

            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnUserAccount objTestClass)
        {
            DAL.DapperRepository<DmnUserAccount> objDapperRepository = new DAL.DapperRepository<DmnUserAccount>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UserAccountId", objTestClass.UserAccountId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool AddUserPermission(string procedure, DmnForms objTestClass)
        {
            DAL.DapperRepository<DmnForms> objdapperRepository = new DAL.DapperRepository<DmnForms>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@id", objTestClass.id);
            parms.Add("@Form", objTestClass.Form);
            parms.Add("@Write", objTestClass.Write);
            parms.Add("@Read", objTestClass.Read);
            parms.Add("@Status", objTestClass.Status);
            parms.Add("@UserType", objTestClass.UserType);
            parms.Add("@CreatedBy", objTestClass.CreatedBy);
            parms.Add("@Note", objTestClass.Note);
            parms.Add("@FormType", objTestClass.FormType);
            return objdapperRepository.Add(procedure, parms);
        }
        public bool AddUserType(string procedure, DmnUserAccountType objTestClass)
        {
            DAL.DapperRepository<DmnForms> objdapperRepository = new DAL.DapperRepository<DmnForms>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@UserAccountTypeId", objTestClass.UserAccountTypeId);
            parms.Add("@UserAccountType", objTestClass.UserAccountType);
            parms.Add("@CreatedBy", objTestClass.CreatedBy);
            parms.Add("@Note", objTestClass.Note);
            parms.Add("@Status", objTestClass.Status);
            return objdapperRepository.Add(procedure, parms);
        }

    }
}
