using Dapper;
using Domain;
using System;

namespace BLL
{
    public class BllPartyBooking
    {
        public DmnPartyBooking AddEditData(string procedureName, DmnPartyBooking obj)
        {
            DAL.DapperRepository<DmnPartyBooking> repo = new DAL.DapperRepository<DmnPartyBooking>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("PartyBookingId", obj.PartyBookingId);
            parms.Add("PartyPackId", obj.PartyPackId);

            parms.Add("CusId", obj.CustomerId);
            parms.Add("CustomerName", obj.CustomerName);
            parms.Add("Phone", obj.Phone);
            parms.Add("Email", obj.Email);
            parms.Add("Address", obj.Address);

            parms.Add("BookingDateTime", obj.BookingDateTime);

            parms.Add("EventType", obj.EventType);

            parms.Add("NoOfAdults", obj.NoOfAdults);
            parms.Add("NoOfChildren", obj.NoOfChildren);

            parms.Add("PackPrice", obj.PackPrice);

            parms.Add("Tax", obj.Tax);
            parms.Add("TaxType", obj.TaxType);
            parms.Add("TaxPer", obj.TaxPer);
            parms.Add("TaxAmount", obj.TaxAmount);
            parms.Add("IGSTAmt", obj.IGSTAmt);
            parms.Add("CGSTAmt", obj.CGSTAmt);
            parms.Add("SGSTAmt", obj.SGSTAmt);
            parms.Add("VATAmt", obj.VATAmt);

            parms.Add("DiscountPer", obj.DiscountPer);
            parms.Add("DiscountAmount", obj.DiscountAmount);

            parms.Add("PreTaxAmount", obj.PreTaxAmount);
            parms.Add("PostTaxAmount", obj.PostTaxAmount);

            parms.Add("NetAmount", obj.NetAmount);
            parms.Add("AdvanceAmount", obj.AdvanceAmount);
            parms.Add("ReceivedAmount", obj.ReceivedAmount);
            parms.Add("BalanceAmount", obj.BalanceAmount);

            parms.Add("PaymentMode", obj.PaymentMode);

            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("Status", obj.Status);

            parms.Add("DiscountReason", obj.DiscountReason);
            parms.Add("EventDescription", obj.EventDescription);

            // 🔹 CAKE TOTALS
            parms.Add("CakeGrandTotal", obj.CakeGrandTotal);
            parms.Add("CakePreTaxAmount", obj.CakePreTaxAmount);
            parms.Add("CakeTaxAmount", obj.CakeTaxAmount);

            // 🔹 CARD TOTALS
            parms.Add("CardGrandTotal", obj.CardGrandTotal);
            parms.Add("CardPreTaxAmount", obj.CardPreTaxAmount);
            parms.Add("CardTaxAmount", obj.CardTaxAmount);

            // 🔹 GIFT TOTALS
            parms.Add("GiftGrandTotal", obj.GiftGrandTotal);
            parms.Add("GiftPreTaxAmount", obj.GiftPreTaxAmount);
            parms.Add("GiftTaxAmount", obj.GiftTaxAmount);

            // 🔹 PACKAGE TOTALS
            parms.Add("PackGrandTotal", obj.PackGrandTotal);
            parms.Add("PackPreTaxAmount", obj.PackPreTaxAmount);
            parms.Add("PackTaxAmount", obj.PackTaxAmount);

            // 🔹 ADDITIONAL CHARGE
            parms.Add("AdditionalChargeAmt", obj.AdditionalChargeAmt);
            parms.Add("AdditionalChargeDescription", obj.AdditionalChargeDescription);

            parms.Add("ExtraChargeGrandTotal", obj.ExtraChargeGrandTotal);
            parms.Add("ExtraChargePreTaxAmount", obj.ExtraChargePreTaxAmount);
            parms.Add("ExtraChargeTaxAmount", obj.ExtraChargeTaxAmount);

            parms.Add("KoTPrintStatus_counter", obj.KoTPrintStatus_counter);
            parms.Add("KoTPrintStatus_Kitchen", obj.KoTPrintStatus_Kitchen);
            parms.Add("ConsumedRawMaterialStatus", obj.ConsumedRawMaterialStatus);
            parms.Add("KOTDatetime", obj.KOTDatetime);

            var data = repo.AddandReturnID(procedureName, parms);
            return data;
        }

        public DmnPartyBookingDetails AddEditDataDetails(string procedureName, DmnPartyBookingDetails obj)
        {
            DAL.DapperRepository<DmnPartyBookingDetails> repo = new DAL.DapperRepository<DmnPartyBookingDetails>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("PartyBookingDetailsId", obj.PartyBookingDetailsId);
            parms.Add("PartyBookingId", obj.PartyBookingId);

            parms.Add("ProductId", obj.ProductId);
            parms.Add("ProductType", obj.ProductType);
            parms.Add("TableType", obj.TableType);

            parms.Add("Qty", obj.Qty);
            parms.Add("Price", obj.Price);
            parms.Add("Amount", obj.Amount);

            parms.Add("Tax", obj.Tax);
            parms.Add("TaxType", obj.TaxType);
            parms.Add("TaxPer", obj.TaxPer);
            parms.Add("TaxAmount", obj.TaxAmount);
            parms.Add("TaxSettingId", obj.TaxSettingId);

            parms.Add("IGSTAmt", obj.IGSTAmt);
            parms.Add("VATAmt", obj.VATAmt);
            parms.Add("CGSTAmt", obj.CGSTAmt);
            parms.Add("SGSTAmt", obj.SGSTAmt);

            parms.Add("DiscountPer", obj.DiscountPer);
            parms.Add("DiscountAmount", obj.DiscountAmount);

            parms.Add("PreTaxAmount", obj.PreTaxAmount);
            parms.Add("PostTaxAmount", obj.PostTaxAmount);
            parms.Add("NetAmount", obj.NetAmount);

            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("Status", obj.Status);

            parms.Add("Prid", obj.Prid);
            parms.Add("Product", obj.Product);

            parms.Add("CakeRemarks", obj.CakeRemarks);
            parms.Add("CakeMessage", obj.CakeMessage);

            parms.Add("IsPackage", obj.IsPackage);

            parms.Add("CategoryName", obj.CategoryName);

            parms.Add("DefaultpackItems", obj.DefaultpackItems);
            parms.Add("SalesMasterId", obj.SalesMasterId);

            var data = repo.AddandReturnID(procedureName, parms);
            return data;
        }

        public DmnPartyBooking VoidData(string procedureName, DmnPartyBooking obj)
        {
            DAL.DapperRepository<DmnPartyBooking> repo = new DAL.DapperRepository<DmnPartyBooking>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("PartyBookingId", obj.PartyBookingId);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("Status", obj.Status);

            var data = repo.AddandReturnID(procedureName, parms);
            return data;
        }

    }

}