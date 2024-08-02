using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Domain;

namespace BLL
{
    public class BllPickup
    {
        public DmnPickup AddData(string procedureName, DmnPickup objTestClass)
        {
            DAL.DapperRepository<DmnPickup> objDapperRepository = new DAL.DapperRepository<DmnPickup>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PkId", objTestClass.PkId);
            parms.Add("CusName", objTestClass.CusName);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Address", objTestClass.Address);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("TotalQuantity", objTestClass.TotalQuantity);
            parms.Add("DiscountAmt", objTestClass.DiscountAmt);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("OtherAdd", objTestClass.OtherAdd);
            parms.Add("OtherDed", objTestClass.OtherDed);
            parms.Add("OtherAddText", objTestClass.OtherAddText);
            parms.Add("OtherDedText", objTestClass.OtherDedText);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("CashRev", objTestClass.CashRev);
            parms.Add("BalnceAmt", objTestClass.BalnceAmt);
            parms.Add("RemoveNotes", objTestClass.RemoveNotes);
            parms.Add("SalesMasterId", objTestClass.SalesMasterId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
