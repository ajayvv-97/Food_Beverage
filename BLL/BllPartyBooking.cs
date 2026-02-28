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

            parms.Add("BookingDate", obj.BookingDate);
            parms.Add("BookingTime", obj.BookingTime);
            parms.Add("EventType", obj.EventType);

            parms.Add("NoOfAdults", obj.NoOfAdults);
            parms.Add("NoOfChildren", obj.NoOfChildren);

            parms.Add("PackPrice", obj.PackPrice);

            parms.Add("Tax", obj.Tax);
            parms.Add("TaxType", obj.TaxType);
            parms.Add("TaxPer", obj.TaxPer);
            parms.Add("TaxAmount", obj.TaxAmount);

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

            parms.Add("Tax", obj.Tax);
            parms.Add("TaxType", obj.TaxType);
            parms.Add("TaxPer", obj.TaxPer);
            parms.Add("TaxAmount", obj.TaxAmount);
            parms.Add("TaxSettingId", obj.TaxSettingId);

            parms.Add("DiscountPer", obj.DiscountPer);
            parms.Add("DiscountAmount", obj.DiscountAmount);

            parms.Add("PreTaxAmount", obj.PreTaxAmount);
            parms.Add("NetAmount", obj.NetAmount);

            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("Status", obj.Status);

            var data = repo.AddandReturnID(procedureName, parms);
            return data;
        }

    }
    
}