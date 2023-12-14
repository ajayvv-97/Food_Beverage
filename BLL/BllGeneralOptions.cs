using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllGeneralOptions
    {
        public bool AddData(string procedureName, DmnGeneralOptions objTestClass)
        {
            DAL.DapperRepository<DmnGeneralOptions> objDapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Caption", objTestClass.Caption);
            parms.Add("GTypeId", objTestClass.GTypeId);
            //parms.Add("GType", objTestClass.GType);
            // parms.Add("GTypeSubId", objTestClass.GTypeSubId);//need to increment based on entry need to remove this field
            parms.Add("GTypeSubValue1", objTestClass.GTypeSubValue1);
            parms.Add("GTypeSubValue2", objTestClass.GTypeSubValue2);
            parms.Add("GTypeSubValue3", objTestClass.GTypeSubValue3);
            parms.Add("GTypeSubText1", objTestClass.GTypeSubText1);
            parms.Add("GTypeSubText2", objTestClass.GTypeSubText2);
            parms.Add("GTypeSubText3", objTestClass.GTypeSubText3);
            parms.Add("GTypeSubDate1", objTestClass.GTypeSubDate1);
            parms.Add("GTypeSubDate2", objTestClass.GTypeSubDate2);
            parms.Add("GTypeSubDate3", objTestClass.GTypeSubDate3);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }

        public bool AddComboData(string procedureName, DmnGeneralOptions objTestClass)
        {
            DAL.DapperRepository<DmnGeneralOptions> objDapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Caption", objTestClass.Caption);
            parms.Add("GTypeId", objTestClass.GTypeId);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnGeneralOptions getDataById(string procedureName, DmnGeneralOptions objTestClass)
        {
            DAL.DapperRepository<DmnGeneralOptions> objDapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GeneralId", objTestClass.GeneralId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnGeneralOptions objTestClass)
        {
            DAL.DapperRepository<DmnGeneralOptions> objDapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GeneralId", objTestClass.GeneralId);
            parms.Add("Caption", objTestClass.Caption);
            parms.Add("GTypeId", objTestClass.GTypeId);
            //parms.Add("GType", objTestClass.GType);
            // parms.Add("GTypeSubId", objTestClass.GTypeSubId); change it to autogenerate
            parms.Add("GTypeSubValue1", objTestClass.GTypeSubValue1);
            parms.Add("GTypeSubValue2", objTestClass.GTypeSubValue2);
            parms.Add("GTypeSubValue3", objTestClass.GTypeSubValue3);
            parms.Add("GTypeSubText1", objTestClass.GTypeSubText1);
            parms.Add("GTypeSubText2", objTestClass.GTypeSubText2);
            parms.Add("GTypeSubText3", objTestClass.GTypeSubText3);
            parms.Add("GTypeSubDate1", objTestClass.GTypeSubDate1);
            parms.Add("GTypeSubDate2", objTestClass.GTypeSubDate2);
            parms.Add("GTypeSubDate3", objTestClass.GTypeSubDate3);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnGeneralOptions objTestClass)
        {
            DAL.DapperRepository<DmnGeneralOptions> objDapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GeneralId", objTestClass.GeneralId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
        public bool AddTransportation(string procedureName, DmnGeneralOptions dmnGeneralOptions)
        {
            DAL.DapperRepository<DmnGeneralOptions> dapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("GTypeSubText1", dmnGeneralOptions.GTypeSubText1);
            parms.Add("GTypeSubText2", dmnGeneralOptions.GTypeSubText2);
            parms.Add("CreatedBy", dmnGeneralOptions.CreatedBy);
            var lstData = dapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public bool DeleteTransportation(string procedureName, DmnGeneralOptions dmnGeneralOptions)
        {
            DAL.DapperRepository<DmnGeneralOptions> dapperRepository = new DAL.DapperRepository<DmnGeneralOptions>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LedgerCode", dmnGeneralOptions.GTypeSubText1);
            var lstData = dapperRepository.Remove(procedureName, parms);
            return lstData;
        }

    }
}
