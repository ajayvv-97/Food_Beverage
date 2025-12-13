using System;
using Dapper;
using Domain;

namespace BLL
{
    public class BllCutomerAccount
    {
        public DmnAccountLedger AddEditCustomer(string procedureName, DmnAccountLedger objCustomer)
        {
            DAL.DapperRepository<DmnAccountLedger> dapperRepository = new DAL.DapperRepository<DmnAccountLedger>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("@CusId", objCustomer.CusId);
            parms.Add("@FirstName", objCustomer.FirstName);
            parms.Add("@SecondName", objCustomer.SecondName);
            parms.Add("@Gender", objCustomer.Gender);

            parms.Add("@PhoneNumber", objCustomer.PhoneNumber);
            parms.Add("@EmailAddress", objCustomer.EmailAddress);

            parms.Add("@CompanyName", objCustomer.CompanyName);
            parms.Add("@GSTNo", objCustomer.GSTNo);

            parms.Add("@FullAddress", objCustomer.FullAddress);

            parms.Add("@Status", objCustomer.Status);
            parms.Add("@BranchId", objCustomer.BranchId);

            parms.Add("@CreatedBy", objCustomer.CreatedBy);
            parms.Add("@SalesMasterId", objCustomer.SalesMasterId);
            var result = dapperRepository.AddandReturnID(procedureName, parms);
            return result;
        }
    }
}
