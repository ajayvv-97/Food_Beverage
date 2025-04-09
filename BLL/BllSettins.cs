
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllSettins
    {
        public DmnSetting AddEditDataSetting(string procedureName, DmnSetting objTestClass)
        {
            DAL.DapperRepository<DmnSetting> dapperRepository = new DAL.DapperRepository<DmnSetting>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SId", objTestClass.SId);
            parms.Add("APPColor", objTestClass.APPColor);
            parms.Add("MenuButtonColor", objTestClass.MenuButtonColor);
            parms.Add("OperationButtonColor", objTestClass.OperationButtonColor);
            parms.Add("Description", objTestClass.Description);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnSetting AddEditDataSetting_msg(string procedureName, DmnSetting objTestClass)
        {
            DAL.DapperRepository<DmnSetting> dapperRepository = new DAL.DapperRepository<DmnSetting>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SId", objTestClass.SId);
            parms.Add("Gmail", objTestClass.Gmail);
            parms.Add("Whatsapp", objTestClass.Whatsapp);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnSetting AddEditDataSetting_bill(string procedureName, DmnSetting objTestClass)
        {
            DAL.DapperRepository<DmnSetting> dapperRepository = new DAL.DapperRepository<DmnSetting>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("SId", objTestClass.SId);
            parms.Add("QRcode", objTestClass.QRcode); 
            parms.Add("Logopath", objTestClass.Logopath);
            parms.Add("Logostatus", objTestClass.Logostatus);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Footermsg", objTestClass.Footermsg);
            parms.Add("Customer_Status", objTestClass.Customer_Status);            
             var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
