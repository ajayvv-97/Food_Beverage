
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllItem
    {
        public DmnItem AddData(string procedureName, DmnItem objTestClass)
        {
            DAL.DapperRepository<DmnItem> objDapperRepository = new DAL.DapperRepository<DmnItem>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("ItemId", objTestClass.ItemId);
            parms.Add("Itemname", objTestClass.Itemname);
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("price", objTestClass.price);
            parms.Add("quantity", objTestClass.quantity);
            parms.Add("unit", objTestClass.unit);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}