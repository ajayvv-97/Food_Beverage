using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Bllgeneralsetting
    {
        public DmnGeneralSetting AddEditDataGen(string procedureName, DmnGeneralSetting objTestClass)
        {
            DAL.DapperRepository<DmnGeneralSetting> dapperRepository = new DAL.DapperRepository<DmnGeneralSetting>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Gid", objTestClass.Gid);
            parms.Add("printdal", objTestClass.printdal);
            parms.Add("xyprintdal", objTestClass.xyprintdal);
            parms.Add("dalydal", objTestClass.dalydal);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("EnableRFID", objTestClass.EnableRFID);
            parms.Add("RequireCustomerInfo", objTestClass.RequireCustomerInfo);
            parms.Add("RequireCustomerInfo_recharge", objTestClass.RequireCustomerInfo_recharge);

            parms.Add("EnableDefaultGmail", objTestClass.EnableDefaultGmail);
            parms.Add("DefaultGmail", objTestClass.DefaultGmail);
            parms.Add("AutoPrint", objTestClass.AutoPrint);
            parms.Add("AutoDrawer", objTestClass.AutoDrawer);

            parms.Add("Save_YZout", objTestClass.Save_YZout);
            parms.Add("Save_Daily", objTestClass.Save_Daily);
            parms.Add("YZPath", objTestClass.YZPath);
            parms.Add("DailyPath", objTestClass.DailyPath);

            parms.Add("EnableGroupCardInPayment", objTestClass.EnableGroupCardInPayment);
            parms.Add("EnableFamilyCardInPayment", objTestClass.EnableFamilyCardInPayment);
            parms.Add("EnableButtonView_GroupSale", objTestClass.EnableButtonView_GroupSale);
            parms.Add("EnableButtonView_Family", objTestClass.EnableButtonView_Family);
            parms.Add("AllowManualCardInputDuringPayment", objTestClass.AllowManualCardInputDuringPayment);
            parms.Add("AutoLogoutWhenIdle", objTestClass.AutoLogoutWhenIdle);
            parms.Add("AutoLogoutIdleTimeoutMinutes", objTestClass.AutoLogoutIdleTimeoutMinutes);

            parms.Add("LogoutFormscustom", objTestClass.LogoutFormscustom);
            parms.Add("DefaultUserName", objTestClass.DefaultUserName);
            parms.Add("EnableUserPrompt", objTestClass.EnableUserPrompt);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnGeneralSetting AddEditDataGenConf(string procedureName, DmnGeneralSetting objTestClass)
        {
            DAL.DapperRepository<DmnGeneralSetting> dapperRepository = new DAL.DapperRepository<DmnGeneralSetting>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Gid", objTestClass.Gid);
            parms.Add("Status", objTestClass.Status);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("EmployeeId", objTestClass.EmployeeId);
            parms.Add("BranchId", objTestClass.BranchId);

            parms.Add("PrintBillType", objTestClass.PrintBillType);
            parms.Add("BillTaxType", objTestClass.BillTaxType);
            parms.Add("FooterContent", objTestClass.FooterContent);
            parms.Add("PhoneNoLength", objTestClass.PhoneNoLength);
            parms.Add("ZipCodeLength", objTestClass.ZipCodeLength);

            parms.Add("CompanyNameOtherLanguage", objTestClass.CompanyNameOtherLanguage);
            parms.Add("CompanyAddressOtherLanguage", objTestClass.CompanyAddressOtherLanguage);
            parms.Add("FooterOtherLanguage", objTestClass.FooterOtherLanguage);

            parms.Add("Logofile", objTestClass.BillLogoicon);
            parms.Add("Logostatus", objTestClass.Logostatus);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
