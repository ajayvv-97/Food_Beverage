using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Bllvendor
    {
        public DmnVendor AddEditDataVendor(string procedureName, DmnVendor objTestClass)
        {
            DAL.DapperRepository<DmnVendor> dapperRepository = new DAL.DapperRepository<DmnVendor>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("VendorId", objTestClass.VendorId);
            parms.Add("VendorName", objTestClass.VendorName);
            parms.Add("Address", objTestClass.Address);
            parms.Add("Telephone", objTestClass.Telephone);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Description", objTestClass.Description);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
