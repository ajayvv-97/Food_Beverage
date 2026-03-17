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
            parms.Add("IsDirectSale", objTestClass.IsDirectSale);

            // ---------------------------
            // NEW CUSTOMER DETAILS (ADDED)
            // ---------------------------
            parms.Add("FirstName", objTestClass.FirstName);
            parms.Add("SecondName", objTestClass.SecondName);
            parms.Add("CompanyName", objTestClass.CompanyName);
            parms.Add("GSTNo", objTestClass.GSTNo);
            parms.Add("PhoneNumber", objTestClass.PhoneNumber);
            parms.Add("EmailAddress", objTestClass.EmailAddress);
            parms.Add("FullAddress", objTestClass.FullAddress);
            parms.Add("Gender", objTestClass.Gender);
            parms.Add("Cusid", objTestClass.cusid);
            parms.Add("TotalPreTaxAmt", objTestClass.TotalPreTaxAmt);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }        
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
            parms.Add("IsStockTracked", objTestClass.IsStockTracked);
            parms.Add("PreTaxAmt", objTestClass.PreTaxAmt);
            //parms.Add("Pkid", objTestClass.Pkid);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;

        }

        public DmnSalemaster AddEditPartyBookingSale(string procedureName, DmnSalemaster obj)
        {
            DAL.DapperRepository<DmnSalemaster> dapperRepository =
                new DAL.DapperRepository<DmnSalemaster>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("@SalesMasterId", obj.SalesMasterId);
            parms.Add("@EntryNo", obj.EntryNo);
            parms.Add("@EntryDate", obj.EntryDate);

            parms.Add("@TotalQuantity", obj.TotQty);
            parms.Add("@TotalAmount", obj.TotalAmnt);

            parms.Add("@TotalDiscount", obj.TotalDiscount);
            parms.Add("@DiscountPer", obj.DisPer);
            parms.Add("@DiscountAmount", obj.DisAmnt);

            parms.Add("@NetAmount", obj.NetAmount);

            parms.Add("@TotalTax", obj.TotalTax);
            parms.Add("@TotalIGST", obj.TotalIGST);
            parms.Add("@TotalCGST", obj.TotalCGST);
            parms.Add("@TotalSGST", obj.TotalSGST);
            parms.Add("@TotalVAT", obj.TotalVAT);

            parms.Add("@RcvAmnt", obj.RcvAmnt);
            parms.Add("@BalAmnt", obj.BalAmnt);

            parms.Add("@PaymentMode", obj.PayId);

            parms.Add("@PartyBookingId", obj.PartyBookingId);

            // Customer
            parms.Add("@FirstName", obj.FirstName);
            parms.Add("@SecondName", obj.SecondName);
            parms.Add("@CompanyName", obj.CompanyName);
            parms.Add("@PhoneNumber", obj.PhoneNumber);
            parms.Add("@EmailAddress", obj.EmailAddress);

            parms.Add("@CusId", obj.cusid);

            parms.Add("@TotalPreTaxAmt", obj.TotalPreTaxAmt);

            parms.Add("@SaleType", 2);

            parms.Add("@CreatedBy", obj.CreatedBy);
            parms.Add("@BranchId", obj.BranchId);
            parms.Add("@Status", obj.Status);

            parms.Add("@mDeviceIP", obj.mDeviceIP);
            parms.Add("@mDevice", obj.mDevice);

            parms.Add("AdditionalChargeDescription", obj.AdditionalChargeDescription);
            parms.Add("AdditionalChargeAmt", obj.AdditionalChargeAmt);

            var data = dapperRepository.AddandReturnID(procedureName, parms);

            return data;
        }

        public DmnSaledetails AddEditPartyBookingSaleDetails(string procedureName, DmnSaledetails obj)
        {
            DAL.DapperRepository<DmnSaledetails> dapperRepository =
                new DAL.DapperRepository<DmnSaledetails>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("@SalesDetailsId", obj.SalesDetailsId);
            parms.Add("@SalesMasterId", obj.SalesMasterId);

            parms.Add("@PartyBookingDetailsId", obj.PartyBookingDetailsId);

            parms.Add("@ItemName", obj.ItemName);
            parms.Add("@ItemId", obj.itemid);
            parms.Add("@ProductType", obj.ProductType);

            parms.Add("@Quantity", obj.Quantity);
            parms.Add("@SalesRate", obj.SalesRate);
            parms.Add("@Amount", obj.Amount);

            parms.Add("@DiscountPer", obj.DiscountPer);
            parms.Add("@DiscountAmount", obj.DiscountAmount);

            parms.Add("@Tax", obj.Tax);
            parms.Add("@TaxType", obj.TaxType);
            parms.Add("@TaxPer", obj.TaxPer);
            parms.Add("@TaxAmount", obj.TaxAmount);

            parms.Add("@IGSTAmount", obj.IGSTAmount);
            parms.Add("@CGSTAmount", obj.CGSTAmount);
            parms.Add("@SGSTAmount", obj.SGSTAmount);
            parms.Add("@VatAmount", obj.VatAmount);

            parms.Add("@PreTaxAmt", obj.PreTaxAmt);
            parms.Add("@NetAmount", obj.NetAmount);

            parms.Add("@BranchId", obj.BranchId);
            parms.Add("@CreatedBy", obj.CreatedBy);
            parms.Add("@Status", obj.Status);

            // NEW PARAMETERS
            parms.Add("@IsPackage", obj.IsPackage);
            parms.Add("@CakeMessage", obj.CakeMessage);
            parms.Add("@PackageType", obj.PackageType);
            parms.Add("@PartyBookingId", obj.PartyBookingId);

            var data = dapperRepository.AddandReturnID(procedureName, parms);

            return data;
        }

        public DmnSalemaster AddEditSplit(string procedureName, DmnSalemaster objTestClass)
        {
            DAL.DapperRepository<DmnSalemaster> dapperRepository = new DAL.DapperRepository<DmnSalemaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CardSalesMasterId", objTestClass.SalesMasterId);
            parms.Add("PartPayId", objTestClass.PartPayId);
            parms.Add("PartAmount", objTestClass.PartAmount);
          
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("DebitLedgerId", objTestClass.DebitLedgerId);
            parms.Add("DebitICardNo", objTestClass.DebitICardNo);
            parms.Add("CardType", objTestClass.CardType);
            parms.Add("FMCardType", objTestClass.FMCardType);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("mDevice", objTestClass.mDevice);
            parms.Add("mDeviceIP", objTestClass.mDeviceIP);
            parms.Add("FormType", objTestClass.FormType);
            parms.Add("Status", objTestClass.Status);
            parms.Add("DBname", objTestClass.DBname);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;

        }
        public DmnSalemaster AddEditmidas(string procedureName, DmnSalemaster objTestClass)
        {
            DAL.DapperRepository<DmnSalemaster> dapperRepository = new DAL.DapperRepository<DmnSalemaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("DebitLedgerId", objTestClass.DebitLedgerId);
            parms.Add("DebitICardNo", objTestClass.DebitICardNo);
            parms.Add("CardType", objTestClass.CardType);
            parms.Add("FMCardType", objTestClass.FMCardType);
            parms.Add("Amount", objTestClass.NetAmount);         
            parms.Add("DBname", objTestClass.DBname);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;

        }
        public DmnSaledetails AddEditstock(string procedureName, DmnSaledetails objTestClass)
        {
            DAL.DapperRepository<DmnSaledetails> dapperRepository = new DAL.DapperRepository<DmnSaledetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesDetailsId", objTestClass.SalesDetailsId);
            parms.Add("QTY", objTestClass.Quantity);
            parms.Add("itemid", objTestClass.itemid);        
            parms.Add("TableType", objTestClass.TableType);
            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("Prid", objTestClass.Prid);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;


        }

        public DmnPaymentAdjustments AddEditData_SalesMaster_paymentadj(string procedureName, DmnPaymentAdjustments objTestClass)
        {
            DAL.DapperRepository<DmnPaymentAdjustments> dapperRepository = new DAL.DapperRepository<DmnPaymentAdjustments>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CardSalesMasterId", objTestClass.CardSalesMasterId);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("Remark", objTestClass.Remark);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("SuperadminId", objTestClass.SuperadminId);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("PayId", objTestClass.PayId);
            parms.Add("SaleType", objTestClass.SaleType);
            parms.Add("mDeviceIP", objTestClass.mDeviceIP);
            parms.Add("SaleEmpid", objTestClass.SaleEmpid);
            parms.Add("Frompayid", objTestClass.Frompayid);           
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
    }
}

