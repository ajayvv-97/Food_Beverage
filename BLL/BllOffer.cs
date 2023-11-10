using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllOffer
    {

        public DmnOfferMaster AddEditDataM(string procedureName, DmnOfferMaster objTestClass)
        {
            DAL.DapperRepository<DmnOfferMaster> dapperRepository = new DAL.DapperRepository<DmnOfferMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Omid", objTestClass.Omid);
            parms.Add("OfferName", objTestClass.OfferName);
            parms.Add("OfferType", objTestClass.OfferType);
            parms.Add("CreatedBy", objTestClass.CreatedBy); 
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("TotalPrice", objTestClass.TotalPrice);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        public DmnOfferDetails AddEditDataD(string procedureName, DmnOfferDetails objTestClass)
        {
            DAL.DapperRepository<DmnOfferDetails> dapperRepository = new DAL.DapperRepository<DmnOfferDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Odid", objTestClass.Odid);
            parms.Add("Omid", objTestClass.Omid);
            parms.Add("ProductId", objTestClass.ProductId);
            parms.Add("Discountper", objTestClass.Discountper);
            parms.Add("DiscountAmt", objTestClass.DiscountAmt);
            parms.Add("FinalPrice", objTestClass.FinalPrice);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("Description", objTestClass.Description);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
