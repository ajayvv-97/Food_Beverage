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
            parms.Add("OfferstartDatetime", objTestClass.OfferstartDatetime);
            parms.Add("OfferEndDatetime", objTestClass.OfferEndDatetime);
            parms.Add("DiscountAmt", objTestClass.DiscountAmt);
            parms.Add("Discountper", objTestClass.Discountper);
            parms.Add("FinalPrice", objTestClass.FinalPrice);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Description", objTestClass.Description);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
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
            parms.Add("Prid", objTestClass.Prid);
            parms.Add("Productname", objTestClass.Productname);
            parms.Add("Producttype", objTestClass.Producttype);
            parms.Add("Productname", objTestClass.Productname);
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Price", objTestClass.Price);
            parms.Add("ItemCode_UPC", objTestClass.ItemCode_UPC);

            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("AV_id", objTestClass.AV_id);
            parms.Add("Addoncount", objTestClass.Addoncount);
            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("Srate", objTestClass.Srate);
            parms.Add("VariatiionName", objTestClass.VariatiionName);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        public DmnOfferDetails Removeoffer(string procedureName, DmnOfferDetails objTestClass)
        {
            DAL.DapperRepository<DmnOfferDetails> dapperRepository = new DAL.DapperRepository<DmnOfferDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Odid", objTestClass.Odid);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
