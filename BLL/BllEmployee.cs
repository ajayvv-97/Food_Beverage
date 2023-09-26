using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllEmployee
    {
        public DmnEmployee AddData(string procedureName, DmnEmployee objTestClass)
        {
            DAL.DapperRepository<DmnEmployee> objDapperRepository = new DAL.DapperRepository<DmnEmployee>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeCode", objTestClass.EmployeeCode);
            parms.Add("EmployeeName", objTestClass.EmployeeName);
            parms.Add("GuardianName", objTestClass.GuardianName);
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            parms.Add("DesignationId", objTestClass.DesignationId);
            parms.Add("Qualification", objTestClass.Qualification);
            parms.Add("EmployeeAddress", objTestClass.EmployeeAddress);
            parms.Add("PostalCode", objTestClass.PostalCode);
            parms.Add("DOB", objTestClass.DOB);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("MaritalStatus", objTestClass.MaritalStatus);
            parms.Add("EmployeeStatus", objTestClass.EmployeeStatus);
            parms.Add("ECast", objTestClass.ECast);
            parms.Add("SpouseName", objTestClass.SpouseName);
            parms.Add("PresentAddress", objTestClass.PresentAddress);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("BankId", objTestClass.BankId);
            parms.Add("BankBranch", objTestClass.BankBranch);
            parms.Add("BankAccountNo", objTestClass.BankAccountNo);
            parms.Add("BankIFSC", objTestClass.BankIFSC);
            parms.Add("PFNo", objTestClass.PFNo);
            parms.Add("ESINo", objTestClass.ESINo);
            parms.Add("PFDate", objTestClass.PFDate);
            parms.Add("ESIDate", objTestClass.ESIDate);
            parms.Add("IdentityProof1", objTestClass.IdentityProof1);
            parms.Add("IdentityProof1No", objTestClass.IdentityProof1No);
            parms.Add("IdentityProof1Atch", objTestClass.IdentityProof1Atch);
            parms.Add("IdentityProof2", objTestClass.IdentityProof2);
            parms.Add("IdentityProof2No", objTestClass.IdentityProof2No);
            parms.Add("IdentityProof2Atch", objTestClass.IdentityProof2Atch);
            parms.Add("Experience", objTestClass.Experience);
            parms.Add("TotalExperience", objTestClass.TotalExperience);
            parms.Add("WeeklyOff", objTestClass.WeeklyOff);
            parms.Add("MonthlyLeave", objTestClass.MonthlyLeave);
            parms.Add("BalanceLeave", objTestClass.BalanceLeave);
            parms.Add("TimeIn", objTestClass.TimeIn);
            parms.Add("TimeOut", objTestClass.TimeOut);
            parms.Add("MaxLate", objTestClass.MaxLate);
            parms.Add("LateAffect", objTestClass.LateAffect);
            parms.Add("OverTimeStart", objTestClass.OverTimeStart);
            parms.Add("DateOfJoin", objTestClass.DateOfJoin);
            parms.Add("DateOfResign", objTestClass.DateOfResign);
            parms.Add("ResignReason", objTestClass.ResignReason);
            parms.Add("Salary", objTestClass.Salary);
            parms.Add("PaymentTerm", objTestClass.PaymentTerm);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("RFID_Login_Status", objTestClass.RFID_Login_Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnEmployee getDataById(string procedureName, DmnEmployee objTestClass)
        {
            DAL.DapperRepository<DmnEmployee> objDapperRepository = new DAL.DapperRepository<DmnEmployee>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnEmployee objTestClass)
        {
            DAL.DapperRepository<DmnEmployee> objDapperRepository = new DAL.DapperRepository<DmnEmployee>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("EmployeeCode", objTestClass.EmployeeCode);
            parms.Add("EmployeeName", objTestClass.EmployeeName);
            parms.Add("GuardianName", objTestClass.GuardianName);
            parms.Add("DepartmentId", objTestClass.DepartmentId);
            parms.Add("DesignationId", objTestClass.DesignationId);
            parms.Add("Qualification", objTestClass.Qualification);
            parms.Add("EmployeeAddress", objTestClass.EmployeeAddress);
            parms.Add("PostalCode", objTestClass.PostalCode);
            parms.Add("DOB", objTestClass.DOB);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("MaritalStatus", objTestClass.MaritalStatus);
            parms.Add("EmployeeStatus", objTestClass.EmployeeStatus);
            parms.Add("ECast", objTestClass.ECast);
            parms.Add("SpouseName", objTestClass.SpouseName);
            parms.Add("PresentAddress", objTestClass.PresentAddress);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("BankId", objTestClass.BankId);
            parms.Add("BankBranch", objTestClass.BankBranch);
            parms.Add("BankAccountNo", objTestClass.BankAccountNo);
            parms.Add("BankIFSC", objTestClass.BankIFSC);
            parms.Add("PFNo", objTestClass.PFNo);
            parms.Add("ESINo", objTestClass.ESINo);
            parms.Add("PFDate", objTestClass.PFDate);
            parms.Add("ESIDate", objTestClass.ESIDate);
            parms.Add("IdentityProof1", objTestClass.IdentityProof1);
            parms.Add("IdentityProof1No", objTestClass.IdentityProof1No);
            parms.Add("IdentityProof1Atch", objTestClass.IdentityProof1Atch);
            parms.Add("IdentityProof2", objTestClass.IdentityProof2);
            parms.Add("IdentityProof2No", objTestClass.IdentityProof2No);
            parms.Add("IdentityProof2Atch", objTestClass.IdentityProof2Atch);
            parms.Add("Experience", objTestClass.Experience);
            parms.Add("TotalExperience", objTestClass.TotalExperience);
            parms.Add("WeeklyOff", objTestClass.WeeklyOff);
            parms.Add("MonthlyLeave", objTestClass.MonthlyLeave);
            parms.Add("BalanceLeave", objTestClass.BalanceLeave);
            parms.Add("TimeIn", objTestClass.TimeIn);
            parms.Add("TimeOut", objTestClass.TimeOut);
            parms.Add("MaxLate", objTestClass.MaxLate);
            parms.Add("LateAffect", objTestClass.LateAffect);
            parms.Add("OverTimeStart", objTestClass.OverTimeStart);
            parms.Add("DateOfJoin", objTestClass.DateOfJoin);
            parms.Add("DateOfResign", objTestClass.DateOfResign);
            parms.Add("ResignReason", objTestClass.ResignReason);
            parms.Add("Salary", objTestClass.Salary);
            parms.Add("PaymentTerm", objTestClass.PaymentTerm);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("RFID_Login_Status", objTestClass.RFID_Login_Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnEmployee objTestClass)
        {
            DAL.DapperRepository<DmnEmployee> objDapperRepository = new DAL.DapperRepository<DmnEmployee>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
