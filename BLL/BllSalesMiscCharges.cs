using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Domain;

namespace BLL
{
   public  class BllSalesMiscCharges
    {

        public bool AddData(string procedureName, DmnSalesMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesMasterId", objTestClass.SalesMasterId);
            parms.Add("GeneralId", objTestClass.GeneralId);
            parms.Add("MiscAmount", objTestClass.MiscAmount);
            parms.Add("CreatedBy", objTestClass.CreatedBy);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnSalesMiscCharges getDataById(string procedureName, DmnSalesMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesMiscId", objTestClass.SalesMiscId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnSalesMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GeneralId", objTestClass.GeneralId);//no need to update salesmaster
            parms.Add("MiscAmount", objTestClass.MiscAmount);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnSalesMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesMiscId", objTestClass.SalesMiscId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;


        }

        public bool DeleteMiscCharges(string procedureName, DmnSalesMiscCharges objTestClass)
        {
            DAL.DapperRepository<DmnSalesMiscCharges> objDapperRepository = new DAL.DapperRepository<DmnSalesMiscCharges>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SalesMasterId", objTestClass.SalesMasterId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
