using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllSale
    {
        public DmnSalemaster AddEditData(string procedureName, DmnSalemaster objTestClass)
        {
            DAL.DapperRepository<DmnSalemaster> dapperRepository = new DAL.DapperRepository<DmnSalemaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@SalesMasterId", objTestClass.SalesMasterId);
            parms.Add("@EntryNo", objTestClass.EntryNo);
            parms.Add("@TotQty", objTestClass.TotQty);
            parms.Add("@TotalAmnt", objTestClass.TotalAmnt);
            parms.Add("@TotalDiscount", objTestClass.TotalDiscount);
            parms.Add("@DisPer", objTestClass.DisPer);
            parms.Add("@DisAmnt", objTestClass.DisAmnt);
            parms.Add("@OaAmount", objTestClass.OaAmount);
            parms.Add("@OdAmount", objTestClass.OdAmount);
            parms.Add("@NetAmount", objTestClass.NetAmount);
            parms.Add("@OaDesc", objTestClass.OaDesc);
            parms.Add("@OdDesc", objTestClass.OdDesc);
            parms.Add("@RcvAmnt", objTestClass.RcvAmnt);
            parms.Add("@BalAmnt", objTestClass.BalAmnt);
            parms.Add("@Remark", objTestClass.Remark);
            parms.Add("@CreatedBy", objTestClass.CreatedBy);
            parms.Add("@BranchId", objTestClass.BranchId);
            parms.Add("@Status", objTestClass.Status);
            parms.Add("@TotalTax", objTestClass.TotalTax);
            parms.Add("@TotalIGST", objTestClass.TotalIGST);
            parms.Add("@TotalCGST", objTestClass.TotalCGST);
            parms.Add("@TotalSGST", objTestClass.TotalSGST);
            parms.Add("@TotalVAT", objTestClass.TotalVAT);
            parms.Add("@mDevice", objTestClass.mDevice);
            parms.Add("@mDeviceIP", objTestClass.mDeviceIP);
            parms.Add("@PayId", objTestClass.PayId);
            parms.Add("PromoDId", objTestClass.PromoDId);
            parms.Add("PromoMId", objTestClass.PromoMId);
            parms.Add("Promostatus", objTestClass.Promostatus);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("OrderMasterid", objTestClass.OrderMasterid);
            parms.Add("PromoAmount", objTestClass.PromoAmount);
            parms.Add("CombinedOrderBill_TYPE", objTestClass.CombinedOrderBill_TYPE);
            parms.Add("BookmasterId", objTestClass.BookmasterId);
            parms.Add("ReservationTotal", objTestClass.ReservationTotal);
            parms.Add("AdvanceAmount", objTestClass.AdvanceAmount);

            parms.Add("Name", objTestClass.Name);
            parms.Add("Address", objTestClass.Address);
            parms.Add("ContactNo", objTestClass.ContactNo);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("TimeDuration", objTestClass.TimeDuration);
            parms.Add("Rev_Status", objTestClass.Rev_Status);
            parms.Add("ICardNo", objTestClass.ICardNo);
            parms.Add("CBalance", objTestClass.CBalance);
            parms.Add("TapType", objTestClass.TapType);
            parms.Add("LedgerId", objTestClass.LedgerId);
            parms.Add("BillCopyStatus", objTestClass.BillCopyStatus);
            parms.Add("CombineStatus", objTestClass.CombineStatus);

            parms.Add("PkId", objTestClass.PkId);
            parms.Add("OrderType", objTestClass.OrderType);
            parms.Add("PkChargeAmount", objTestClass.PkChargeAmount);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        //public DmnCardSales AddEditDataCustomer(string procedureName, DmnCardSales objTestClass)
        //{
        //    DAL.DapperRepository<DmnCardSales> dapperRepository = new DAL.DapperRepository<DmnCardSales>();
        //    DynamicParameters parms = new DynamicParameters();
        //    parms.Add("CustomerId", objTestClass.CustomerId);
        //    parms.Add("CustomerName", objTestClass.Customer);
        //    parms.Add("Address", objTestClass.Address);
        //    parms.Add("Telephone", objTestClass.ContactNo);
        //    parms.Add("Email", objTestClass.Email);
        //    parms.Add("CreatedBy", objTestClass.CreatedBy);
        //    parms.Add("ModifiedBy", objTestClass.ModifiedBy);
        //    parms.Add("BranchId", objTestClass.BranchId);
        //    parms.Add("Status", objTestClass.Status);
        //    var lstData = dapperRepository.AddandReturnID(procedureName, parms);
        //    return lstData;
        //}

        public DmnSaledetails AddEditDataDet(string procedureName, DmnSaledetails objTestClass)
        {
            DAL.DapperRepository<DmnSaledetails> dapperRepository = new DAL.DapperRepository<DmnSaledetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesDetailsId", objTestClass.SalesDetailsId);
            parms.Add("SalesMasterId", objTestClass.SalesMasterId);
            parms.Add("MId", objTestClass.MId);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("SalesRate", objTestClass.SalesRate);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("DiscountAmount", objTestClass.DiscountAmount);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("ProductType", objTestClass.ProductType);
            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("cusId", objTestClass.CusId);
            parms.Add("ItemName", objTestClass.ItemName);
            parms.Add("VatAmount", objTestClass.VatAmount);
            parms.Add("Prid", objTestClass.Prid);
            parms.Add("itemid", objTestClass.itemid);
            parms.Add("ItemBarcode", objTestClass.ItemBarcode);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("MarketPrice", objTestClass.MarketPrice);
            parms.Add("TableType", objTestClass.TableType);

            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("AV_id", objTestClass.AV_id);
            parms.Add("Addoncount", objTestClass.Addoncount);
            parms.Add("Cmid", objTestClass.Cmid);
            parms.Add("VariatiionName", objTestClass.VariatiionName);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;

        }
    }
}
