using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Domain;

namespace BLL
{
   public  class BllSalesReturnMiscCharges
    {

        public bool AddData(string procedureName, DmnSalesReturnMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            parms.Add("GeneralId", objTestClass.GeneralId);
            parms.Add("MiscAmount", objTestClass.MiscAmount);
            parms.Add("CreatedBy", objTestClass.CreatedBy);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnSalesReturnMiscCharges getDataById(string procedureName, DmnSalesReturnMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMiscId", objTestClass.SalesReturnMiscId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnSalesReturnMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GeneralId", objTestClass.GeneralId);//no need to update salesmaster
            parms.Add("MiscAmount", objTestClass.MiscAmount);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnSalesReturnMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMiscId", objTestClass.SalesReturnMiscId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;


        }

        public bool DeleteMiscCharges(string procedureName, DmnSalesReturnMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesReturnMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesReturnMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesReturnMasterId", objTestClass.SalesReturnMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
