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
            parms.Add("EntryNo", objTestClass.EntryNo);
            //parms.Add("Customer", objTestClass.CustomerId);
            //parms.Add("Address", objTestClass.Address);
            //parms.Add("ContactNo", objTestClass.ContactNo);
            //parms.Add("Email", objTestClass.Email);
            parms.Add("TotQty", objTestClass.TotalQuantity);
            parms.Add("Remark", objTestClass.Remarks);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Tid", objTestClass.Tid);
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
            bool lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnOrderMaster Remove_order(string procedureName, DmnOrderMaster objTestClass)
        {
            DAL.DapperRepository<DmnOrderMaster> dapperRepository = new DAL.DapperRepository<DmnOrderMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("OrderMasterId", objTestClass.OrderMasterId);
            parms.Add("OrderDetailsId", objTestClass.OrderDetailsId);
            parms.Add("RemoveNotes", objTestClass.RemoveNotes);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("koid", objTestClass.koid);
            parms.Add("Tid", objTestClass.Tid);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
