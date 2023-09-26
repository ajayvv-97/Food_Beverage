using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllItem
    {
        public DmnItem AddData(string procedureName, DmnItem dmnItem)
        {
            DAL.DapperRepository<DmnItem> objDapperRepository = new DAL.DapperRepository<DmnItem>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemCode", dmnItem.ItemCode);
            parms.Add("Barcode", dmnItem.Barcode);
            parms.Add("ItemName", dmnItem.ItemName);
            parms.Add("BrandName", dmnItem.BrandName);
            parms.Add("SearchName", dmnItem.SearchName);
            parms.Add("CategoryId", dmnItem.CategoryId);
            parms.Add("BarcodeMode", dmnItem.BarcodeMode); 
            parms.Add("PacketQty ", dmnItem.PacketQty);
            parms.Add("QuantityOnHand", dmnItem.QuantityOnHand);
            parms.Add("CGSTPer", dmnItem.CGSTPer);
            parms.Add("SGSTPer", dmnItem.SGSTPer);
            parms.Add("IGSTPer", dmnItem.IGSTPer);
            parms.Add("CessPer", dmnItem.CessPer);
            parms.Add("SalesTaxIncl", dmnItem.SalesTaxIncl);
            parms.Add("PurchaseTaxIncl", dmnItem.PurchaseTaxIncl);
            parms.Add("ManufactureId", dmnItem.ManufactureId);
            parms.Add("Rack", dmnItem.Rack);
            parms.Add("SellingPer", dmnItem.SellingPer);
            parms.Add("MainUnitId", dmnItem.MainUnitId);
            parms.Add("PurchaseRate1", dmnItem.PurchaseRate1);
            parms.Add("MRP1", dmnItem.MRP1);
            parms.Add("SubUnitId", dmnItem.SubUnitId);
            parms.Add("PurchaseRate2", dmnItem.PurchaseRate2);
            parms.Add("MRP2", dmnItem.MRP2);
            parms.Add("HSNCode", dmnItem.HSNCode);
            parms.Add("Image", dmnItem.Image);
            parms.Add("BranchId", dmnItem.BranchId);
            parms.Add("TaxType", dmnItem.TaxType);
            parms.Add("LastSellingPer", dmnItem.LastSellingPer);
            parms.Add("LastPurchaseRate1", dmnItem.LastPurchaseRate1);
            parms.Add("LastMRP1", dmnItem.LastMRP1);
            parms.Add("LastPurchaseRate2", dmnItem.LastPurchaseRate2);
            parms.Add("LastMRP2", dmnItem.LastMRP2);
            parms.Add("StockItem", dmnItem.StockItem);
            parms.Add("LockSaleRate", dmnItem.LockSaleRate);
            parms.Add("ItemClassId", dmnItem.ItemClassId);
            parms.Add("ExpiryDate", dmnItem.ExpiryDate);
            parms.Add("Remarks", dmnItem.Remarks);
            parms.Add("CreatedBy", dmnItem.CreatedBy);
            parms.Add("Status", dmnItem.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnItem dmnItem)
        {
            DAL.DapperRepository<DmnItem> objDapperRepository = new DAL.DapperRepository<DmnItem>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemId", dmnItem.ItemId);
            parms.Add("ItemCode", dmnItem.ItemCode);
            parms.Add("Barcode", dmnItem.Barcode);
            parms.Add("ItemName", dmnItem.ItemName);
            parms.Add("BrandName", dmnItem.BrandName);
            parms.Add("SearchName", dmnItem.SearchName);
            parms.Add("CategoryId", dmnItem.CategoryId);
            parms.Add("BarcodeMode", dmnItem.BarcodeMode);
            parms.Add("PacketQty ", dmnItem.PacketQty);
            parms.Add("QuantityOnHand", dmnItem.QuantityOnHand);
            parms.Add("CGSTPer", dmnItem.CGSTPer);
            parms.Add("SGSTPer", dmnItem.SGSTPer);
            parms.Add("IGSTPer", dmnItem.IGSTPer);
            parms.Add("CessPer", dmnItem.CessPer);
            parms.Add("SalesTaxIncl", dmnItem.SalesTaxIncl);
            parms.Add("PurchaseTaxIncl", dmnItem.PurchaseTaxIncl);
            parms.Add("ManufactureId", dmnItem.ManufactureId);
            parms.Add("Rack", dmnItem.Rack);
            parms.Add("SellingPer", dmnItem.SellingPer);
            parms.Add("MainUnitId", dmnItem.MainUnitId);
            parms.Add("PurchaseRate1", dmnItem.PurchaseRate1);
            parms.Add("MRP1", dmnItem.MRP1);
            parms.Add("SubUnitId", dmnItem.SubUnitId);
            parms.Add("PurchaseRate2", dmnItem.PurchaseRate2);
            parms.Add("MRP2", dmnItem.MRP2);
            parms.Add("HSNCode", dmnItem.HSNCode);
            parms.Add("Image", dmnItem.Image);
            parms.Add("BranchId", dmnItem.BranchId);
            parms.Add("TaxType", dmnItem.TaxType);
            parms.Add("LastSellingPer", dmnItem.LastSellingPer);
            parms.Add("LastPurchaseRate1", dmnItem.LastPurchaseRate1);
            parms.Add("LastMRP1", dmnItem.LastMRP1);
            parms.Add("LastPurchaseRate2", dmnItem.LastPurchaseRate2);
            parms.Add("LastMRP2", dmnItem.LastMRP2);
            parms.Add("StockItem", dmnItem.StockItem);
            parms.Add("LockSaleRate", dmnItem.LockSaleRate);
            parms.Add("ItemClassId", dmnItem.ItemClassId);
            parms.Add("ExpiryDate", dmnItem.ExpiryDate);
            parms.Add("Remarks", dmnItem.Remarks);
            parms.Add("ModifiedBy", dmnItem.ModifiedBy);
            parms.Add("ModifiedDateTime", dmnItem.ModifiedDateTime);
            parms.Add("Status", dmnItem.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnItem dmnItem)
        {
            DAL.DapperRepository<DmnItem> objDapperRepository = new DAL.DapperRepository<DmnItem>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemId", dmnItem.ItemId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool UpdateItemFromPurchaseGrid(string procedureName, DmnItem dmnItem)
        {
            DAL.DapperRepository<DmnItem> objDapperRepository = new DAL.DapperRepository<DmnItem>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemId", dmnItem.ItemId);
            parms.Add("UnitId", dmnItem.UnitId);
            parms.Add("FieldName", dmnItem.FieldName);
            parms.Add("FieldValue", dmnItem.FieldValue);
            parms.Add("ModifiedBy", dmnItem.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }

    }
}
