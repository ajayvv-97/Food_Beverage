using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllTaxSettings
    {
        public bool AddData(string procedureName, DmnTaxSettings objTestClass)
        {
            DAL.DapperRepository<DmnTaxSettings> objDapperRepository = new DAL.DapperRepository<DmnTaxSettings>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            parms.Add("TaxType", objTestClass.TaxType);
            parms.Add("IgstPrcnt", objTestClass.IgstPrcnt);
            parms.Add("CgstPrcnt", objTestClass.CgstPrcnt);
            parms.Add("SgstPrcnt", objTestClass.SgstPrcnt);
            parms.Add("VatPrcnt", objTestClass.VatPrcnt);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("TDeafult", objTestClass.TDeafult); 
             var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
    }
}
