using Dapper;
using Domain;

namespace BLL
{
    public class BllVirtualkeyBorad
    {
        public DmnVirtualKeyBoard AddEditDataVirtualkeyboard(string procedureName, DmnVirtualKeyBoard objTestClass)
        {
            DAL.DapperRepository<DmnVirtualKeyBoard> dapperRepository = new DAL.DapperRepository<DmnVirtualKeyBoard>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("Id", objTestClass.Id);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("mDeviceIP", objTestClass.mDeviceIP);

            // === Number Keyboard Columns ===
            parms.Add("Enb_Home_Number", objTestClass.Enb_Home_Number);
            parms.Add("Enb_AllowVirtualKeyboardLogin_Number", objTestClass.Enb_AllowVirtualKeyboardLogin_Number);
            parms.Add("Enb_TablePickUp_Number", objTestClass.Enb_TablePickUp_Number);
            parms.Add("Enb_Operation_Number", objTestClass.Enb_Operation_Number);
            parms.Add("Enb_Employee_Number", objTestClass.Enb_Employee_Number);
            parms.Add("Enb_Billing_Number", objTestClass.Enb_Billing_Number);
            parms.Add("Enb_Report_Number", objTestClass.Enb_Report_Number);
            parms.Add("Enb_OrderPage_Number", objTestClass.Enb_OrderPage_Number);
            parms.Add("Enb_KitchenView_Number", objTestClass.Enb_KitchenView_Number);
            parms.Add("Enb_SearchPage_Number", objTestClass.Enb_SearchPage_Number);
            parms.Add("Enb_SaleBill_YZoutPage_Number", objTestClass.Enb_SaleBill_YZoutPage_Number);

            // === Normal Keyboard Columns ===
            parms.Add("Enb_Home_Normal", objTestClass.Enb_Home_Normal);
            parms.Add("Enb_AllowVirtualKeyboardLogin_Normal", objTestClass.Enb_AllowVirtualKeyboardLogin_Normal);
            parms.Add("Enb_TablePickUp_Normal", objTestClass.Enb_TablePickUp_Normal);
            parms.Add("Enb_Operation_Normal", objTestClass.Enb_Operation_Normal);
            parms.Add("Enb_Employee_Normal", objTestClass.Enb_Employee_Normal);
            parms.Add("Enb_Billing_Normal", objTestClass.Enb_Billing_Normal);
            parms.Add("Enb_Report_Normal", objTestClass.Enb_Report_Normal);
            parms.Add("Enb_OrderPage_Normal", objTestClass.Enb_OrderPage_Normal);
            parms.Add("Enb_KitchenView_Normal", objTestClass.Enb_KitchenView_Normal);
            parms.Add("Enb_SearchPage_Normal", objTestClass.Enb_SearchPage_Normal);
            parms.Add("Enb_SaleBill_YZoutPage_Normal", objTestClass.Enb_SaleBill_YZoutPage_Normal);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
