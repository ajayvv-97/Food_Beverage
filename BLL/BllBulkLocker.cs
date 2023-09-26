using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;

namespace BLL
{
    public class BllBulkLocker
    {   
            public DmnHireLocker AddEditData(string procedureName, DmnHireLocker objTestClass)
        {
            DAL.DapperRepository<DmnHireLocker> objDapperRepository = new DAL.DapperRepository<DmnHireLocker>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BulkLockHireID", objTestClass.BulkLockHireID);
            parms.Add("ExpDate", objTestClass.ExpDate);
            parms.Add("ExpTime", objTestClass.ExpTime);
            parms.Add("HireDate", objTestClass.HireDate);
            parms.Add("HireTime", objTestClass.HireTime);
            parms.Add("Hiredetail", objTestClass.Hiredetail);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("PaymentMode", objTestClass.PaymentMode);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("PaidAmount", objTestClass.PaidAmount);
            parms.Add("BalanceAmount", objTestClass.BalanceAmount);
            parms.Add("AmountAdjust", objTestClass.AmountAdjust);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("LedgerID", objTestClass.LedgerId);
            parms.Add("TotalLocker", objTestClass.TotalLocker);

            parms.Add("AccName", objTestClass.AccName);
            parms.Add("AccAddress", objTestClass.AccAddress);
            parms.Add("AccPhone", objTestClass.AccPhone);
            parms.Add("AccEmail", objTestClass.AccEmail);
            parms.Add("Inotes", objTestClass.Inotes);
            parms.Add("EntryNo", objTestClass.EntryNo);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}

