using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using BLL;
using Dapper;

namespace BLL
{
   public class Bllofferdetails
    {
        public bool AddData(string procedureName, DmnOfferDetails objTestClass)
        {
            DAL.DapperRepository<DmnPurchaseDetails> objDapperRepository = new DAL.DapperRepository<DmnPurchaseDetails>();
            DynamicParameters parms = new DynamicParameters();
           parms.Add("EntryDate", objTestClass.EntryDate);
            parms.Add("OfferDesc", objTestClass.OfferDesc);
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("ItemCode", objTestClass.ItemCode);
            parms.Add("IBarCode", objTestClass.IBarCode);
            parms.Add("ItemName", objTestClass.ItemName);
            parms.Add("pRate", objTestClass.pRate);
            parms.Add("sRate", objTestClass.sRate);
            parms.Add("MRP", objTestClass.MRP);
            parms.Add("OffrRate", objTestClass.OffrRate);
            parms.Add("DiscRate", objTestClass.DiscRate);
            parms.Add("DiscPer", objTestClass.DiscPer);
            parms.Add("BillAmt", objTestClass.BillAmt);
            parms.Add("fDt", objTestClass.fDt);
            parms.Add("tDt", objTestClass.tDt);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Remarks", objTestClass.Remarks);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;

            //public DmnOfferDetails getDataById(string procedureName, DmnOfferDetails objTestClass)
            //{
            //    DAL.DapperRepository<DmnOfferDetails> objDapperRepository = new DAL.DapperRepository<DmnOfferDetails>();
            //    DynamicParameters parms = new DynamicParameters();
            //    parms.Add("ItemId", objTestClass.ItemId);
            //    var lstData = objDapperRepository.FindByID(procedureName, parms);
            //    return lstData;
            //}


        }

        //public List<T> GetSearchData(string Procedure, DmnSearch objSearch)
        //{

        //    DAL.DapperRepository<T> dapper = new DAL.DapperRepository<T>();
        //    DynamicParameters parms = new DynamicParameters();
        //    parms.Add("Select", objSearch.Select);
        //    parms.Add("Table", objSearch.Table);
        //    parms.Add("Where", objSearch.WhereNdGroupBy);
        //    parms.Add("OrderBy", objSearch.OrderBy);
        //    var listData = dapper.SelectQuery(Procedure, parms);
        //    return listData;
        //}
    }
}
