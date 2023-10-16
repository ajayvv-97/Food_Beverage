using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllPurchase
    {
        public List<DmnPurchase> getInvoiceNo(string Procedure, int BranchId)
        {
            DAL.DapperRepository<DmnPurchase> dapper = new DAL.DapperRepository<DmnPurchase>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", BranchId);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }
        public DmnPurchase AddEditData(string procedureName, DmnPurchase objTestClass)
        {
            DAL.DapperRepository<DmnPurchase> dapperRepository = new DAL.DapperRepository<DmnPurchase>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("DocNo", objTestClass.DocNo);
            parms.Add("DocDate", objTestClass.DocDate);
            parms.Add("Customer", objTestClass.CustomerId);
            parms.Add("Address", objTestClass.Address);
            parms.Add("ContactNo", objTestClass.ContactNo);
            parms.Add("Email", objTestClass.Email);
            parms.Add("TotQty", objTestClass.TotQty);
            parms.Add("TotalAmnt", objTestClass.TotalAmnt);
            parms.Add("GrandTot", objTestClass.GrandTot);
            parms.Add("DisAmnt", objTestClass.DisAmnt);
            parms.Add("DisPer", objTestClass.DisPer);
            parms.Add("TotalDiscount", objTestClass.TotalDiscount);
            parms.Add("OaAmount", objTestClass.OaAmount);
            parms.Add("OaDesc", objTestClass.OaDesc);
            parms.Add("OdAmount", objTestClass.OdAmount);
            parms.Add("OdDesc", objTestClass.OdDesc);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("RcvAmnt", objTestClass.RcvAmnt);
            parms.Add("BalAmnt", objTestClass.BalAmnt);
            parms.Add("Terms", objTestClass.Terms);
            parms.Add("Remark", objTestClass.Remark);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnPurchase AddEditDataDet(string procedureName, DmnPurchase objTestClass)
        {
            DAL.DapperRepository<DmnPurchase> dapperRepository = new DAL.DapperRepository<DmnPurchase>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Barcode", objTestClass.Barcode);
            parms.Add("Name", objTestClass.Name);
            parms.Add("Unit", objTestClass.Unit);
            parms.Add("QTY", objTestClass.QTY);
            parms.Add("Prate", objTestClass.Prate);
            parms.Add("Amount", objTestClass.Amount);
            parms.Add("DisPer", objTestClass.DisPer);
            parms.Add("DisAmnt", objTestClass.DisAmnt);
            parms.Add("TotalAmnt", objTestClass.TotalAmnt);
            parms.Add("UnitId", objTestClass.UnitId);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("Srate", objTestClass.Srate);
            parms.Add("Tax", objTestClass.Tax);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("Type", objTestClass.Type);
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            parms.Add("PurchaseDetailId", objTestClass.PurchaseDetailId);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnPurchase AddEditDataVendor(string procedureName, DmnPurchase objTestClass)
        {
            DAL.DapperRepository<DmnPurchase> dapperRepository = new DAL.DapperRepository<DmnPurchase>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("VendorId", objTestClass.CustomerId);
            parms.Add("VendorName", objTestClass.Customer);
            parms.Add("Address", objTestClass.Address);
            parms.Add("Telephone", objTestClass.ContactNo);
            parms.Add("Email", objTestClass.Email);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}