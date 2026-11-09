using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllOrder
    {
        public DmnOrderMaster AddEditData(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("BookmasterId", objTestClass.BookmasterId);
            parms.Add("EntryNo", objTestClass.EntryNo);

            parms.Add("TotQty", objTestClass.TotalQuantity);
            parms.Add("Remark", objTestClass.Remarks);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("PickupOrderFlag", objTestClass.PickupOrderFlag);
            parms.Add("PkId", objTestClass.PkId);
            parms.Add("Kitchennumber", objTestClass.Kitchennumber);
            if (string.Equals(procedureName, "prdInsert_Upd_OrderMaster", StringComparison.OrdinalIgnoreCase))
                parms.Add("OrderTypeId", objTestClass.OrderTypeId);

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
            parms.Add("cusid", objTestClass.cusid);

            // ✅ Guest Counts
            parms.Add("AdultCount", objTestClass.AdultCount);
            parms.Add("ChildCount", objTestClass.ChildCount);
            parms.Add("OtherGuestCount", objTestClass.OtherGuestCount);
            parms.Add("TotalGuestCount", objTestClass.TotalGuestCount);

            parms.Add("OrderPlatformmasterName", objTestClass.OrderPlatformmasterName);
            parms.Add("OrderPlatformmaster", objTestClass.OrderPlatformmaster);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        public DmnOrderMaster RefreshOrderTokenStatus(string procedureName, int orderMasterId, int modifiedBy)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository =
                new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", orderMasterId);
            parms.Add("ModifiedBy", modifiedBy);
            return dapperRepository.AddandReturnID(procedureName, parms);
        }

        public DmnOrderMaster AddData(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("BookmasterId", objTestClass.BookmasterId);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("Address", objTestClass.Address);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("CustomerName", objTestClass.CustomerName);
            parms.Add("Gender", objTestClass.Gender);
            
            parms.Add("Email", objTestClass.Email);
            parms.Add("TotQty", objTestClass.TotalQuantity);
            parms.Add("Remark", objTestClass.Remarks);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("PickupOrderFlag", objTestClass.PickupOrderFlag);
            parms.Add("PkId", objTestClass.PkId);
            parms.Add("Kitchennumber", objTestClass.Kitchennumber);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderDetails AddEditDataDet(string procedureName, DmnOrderDetails objTestClass)
        {
            DAL.DapperRepository<DmnOrderDetails> dapperRepository = new DAL.DapperRepository<DmnOrderDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Barcode", objTestClass.ItemBarCode);
            parms.Add("Name", objTestClass.ItemName);
            parms.Add("Unit", objTestClass.UnitId);
            parms.Add("QTY", objTestClass.Quantity);
            parms.Add("Prate", objTestClass.PurchaseRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DisPer", objTestClass.DiscountPer);
            parms.Add("DisAmnt", objTestClass.DiscountAmount);
            parms.Add("TotalAmnt", objTestClass.NetAmount);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("Srate", objTestClass.Srate);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("PrId", objTestClass.PrId);
            parms.Add("Notes", objTestClass.Notes);
            parms.Add("OrderNo", objTestClass.OrderNo);

            parms.Add("IGSTAmount", objTestClass.IGSTAmount); 
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("VATAmount", objTestClass.VATAmount);
            parms.Add("TableType", objTestClass.TableType);
            parms.Add("MarketPrice", objTestClass.MarketPrice);
            parms.Add("BookingFlag", objTestClass.BookingFlag);
            parms.Add("MainRemarks", objTestClass.MainRemarks);
            parms.Add("KOTPrintStatus", objTestClass.KOTPrintStatus);
            parms.Add("Tid", objTestClass.Tid);

            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("AV_id", objTestClass.AV_id);
            parms.Add("Addoncount", objTestClass.Addoncount);
            parms.Add("Kitchennumber", objTestClass.Kitchennumber);
            parms.Add("vid", objTestClass.vid);
            parms.Add("CCode", objTestClass.CCode);
            parms.Add("VariatiionName", objTestClass.VariatiionName);

            parms.Add("PkId", objTestClass.PkId);

            parms.Add("OrderPlatformdetails", objTestClass.OrderPlatformdetails);
            parms.Add("OrderPlatformdetailsName", objTestClass.OrderPlatformdetailsName);

            parms.Add("orderpriority", objTestClass.orderpriority);
            parms.Add("ReprintStatus", objTestClass.ReprintStatus);

            parms.Add("PreparationMode", objTestClass.PreparationMode);
            parms.Add("IsStockTracked", objTestClass.IsStockTracked);

            parms.Add("PreTaxAmt", objTestClass.PreTaxAmt);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public bool AddDetail(string procedureName, DmnOrderDetails objTestClass)
        {
            DAL.DapperRepository<DmnOrderDetails> objDapperRepository = new DAL.DapperRepository<DmnOrderDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Barcode", objTestClass.ItemBarCode);
            parms.Add("Name", objTestClass.ItemName);
            parms.Add("Unit", objTestClass.UnitId);
            parms.Add("QTY", objTestClass.Quantity);
            parms.Add("Prate", objTestClass.PurchaseRate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DisPer", objTestClass.DiscountPer);
            parms.Add("DisAmnt", objTestClass.DiscountAmount);
            parms.Add("TotalAmnt", objTestClass.NetAmount);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("Srate", objTestClass.Srate);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("PrId", objTestClass.PrId);
            parms.Add("Notes", objTestClass.Notes);
            parms.Add("OrderNo", objTestClass.OrderNo);

            parms.Add("IGSTAmount", objTestClass.IGSTAmount);
            parms.Add("CGSTAmount", objTestClass.CGSTAmount);
            parms.Add("SGSTAmount", objTestClass.SGSTAmount);
            parms.Add("VATAmount", objTestClass.VATAmount);
            parms.Add("TableType", objTestClass.TableType);
            parms.Add("MarketPrice", objTestClass.MarketPrice);
            parms.Add("KOTPrintStatus", objTestClass.KOTPrintStatus);

            parms.Add("Tid", objTestClass.Tid);
            parms.Add("Pavid", objTestClass.Pavid);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("AV_id", objTestClass.AV_id);
            parms.Add("Addoncount", objTestClass.Addoncount);
            parms.Add("vid", objTestClass.vid);
            parms.Add("CCode", objTestClass.CCode);
            parms.Add("VariatiionName", objTestClass.VariatiionName);
            bool lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster Remove_order_ALL(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("OrderCancelNotes", objTestClass.OrderCancelNotes);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("koid", objTestClass.koid);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("Avid", objTestClass.AV_id);
            parms.Add("Sourcepage", objTestClass.Sourcepage);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster Remove_order_KitchenOrder_Start_Rady(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();            
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);         
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("koid", objTestClass.koid);
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderNo", objTestClass.OrderNo);          
            //parms.Add("Tid", objTestClass.Tid);
            //parms.Add("OrderCancelNotes", objTestClass.OrderCancelNotes);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("Avid", objTestClass.AV_id);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster Remove_order_KitchenOrder_ByProduct(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("koid", objTestClass.koid);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("RemoveNotes", objTestClass.RemoveNotes);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("Avid", objTestClass.AV_id);
            parms.Add("CancelNotifyStatus", objTestClass.CancelNotifyStatus);
            parms.Add("KitchenCancelStatus", objTestClass.KitchenCancelStatus);
            parms.Add("Sourcepage", objTestClass.Sourcepage);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster CancelNotifyStatus_update(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("CancelNotifyStatus", objTestClass.CancelNotifyStatus);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("PkId", objTestClass.PkId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster Reconfirmorder_update(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("KitchenCancelStatus", objTestClass.KitchenCancelStatus);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("PkId", objTestClass.PkId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster OrderCancelNotify_update(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("SalesMasterId", objTestClass.SalesMasterId);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("PkId", objTestClass.PkId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster Remove_order_orderview(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("koid", objTestClass.koid);
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("RemoveNotes", objTestClass.RemoveNotes);
            parms.Add("AddonStatus", objTestClass.AddonStatus);
            parms.Add("Avid", objTestClass.AV_id);
            parms.Add("KitchenCancelStatus", objTestClass.KitchenCancelStatus); 
            parms.Add("CancelNotifyStatus", objTestClass.CancelNotifyStatus);    
            
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster ReleaseQty(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        public DmnOrderMaster MsgNotifyStatus_update(string procedureName, DmnOrderMaster objTestClass)   // 1 = OrderMaster, 2 = OrderDetail)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository =new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("@OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("@TableType", objTestClass.TableType); // 1 or 2
            parms.Add("ActionType", objTestClass.ActionType);
            var result = dapperRepository.AddandReturnID(procedureName, parms);
            return result;
        }

    }
}
