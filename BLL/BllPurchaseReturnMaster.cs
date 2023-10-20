using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllPurchaseReturnMaster
    {
        public List<DmnPurchaseMaster> getInvoiceNo(string Procedure, int BranchId)
        {
            DAL.DapperRepository<DmnPurchaseMaster> dapper = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BranchId", BranchId);
            var listData = dapper.SelectQuery(Procedure, parms);
            return listData;
        }
        public DmnPurchaseMaster AddEditData(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseMaster> dapperRepository = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PurchaseReturnMasterId", objTestClass.PurchaseReturnMasterId);
            parms.Add("PurchaseMasterId", objTestClass.PurchaseMasterId);
            parms.Add("EntryNo", objTestClass.EntryNo);
            parms.Add("DocNo", objTestClass.DocNo);
            parms.Add("DocDate", objTestClass.DocDate);
            parms.Add("CustomerId", objTestClass.CustomerId);
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
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnPurchaseMaster AddEditDataDet(string procedureName, DmnPurchaseMaster objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseMaster> dapperRepository = new DAL.DapperRepository<DmnPurchaseMaster>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Product_id", objTestClass.Product_id);
            parms.Add("EntryNo", objTestClass.EntryNo);
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
            parms.Add("Type", objTestClass.Type);
            parms.Add("Typetax", objTestClass.TaxType);
            parms.Add("PurchaseReturnMasterId", objTestClass.PurchaseReturnMasterId);
            parms.Add("PurchaseReturnDetailId", objTestClass.PurchaseReturnDetailId);
            parms.Add("PurchaseDetailId", objTestClass.PurchaseDetailId);
            parms.Add("TaxPer", objTestClass.TaxPer);
            parms.Add("TaxAmount", objTestClass.TaxAmount);
            parms.Add("PrId", objTestClass.PrId);
            parms.Add("Tax", objTestClass.Tax);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
