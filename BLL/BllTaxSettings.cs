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
            parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public DmnTaxSettings getDataById(string procedureName, DmnTaxSettings objTestClass)
        {
            DAL.DapperRepository<DmnTaxSettings> objDapperRepository = new DAL.DapperRepository<DmnTaxSettings>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            var lstData = objDapperRepository.FindByID(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnTaxSettings objTestClass)
        {
            DAL.DapperRepository<DmnTaxSettings> objDapperRepository = new DAL.DapperRepository<DmnTaxSettings>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            parms.Add("IgstPrcnt", objTestClass.IgstPrcnt);
            parms.Add("CgstPrcnt", objTestClass.CgstPrcnt);
            parms.Add("SgstPrcnt", objTestClass.SgstPrcnt);
            parms.Add("VatPrcnt", objTestClass.VatPrcnt);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
        public bool DeleteById(string procedureName, DmnTaxSettings objTestClass)
        {
            DAL.DapperRepository<DmnTaxSettings> objDapperRepository = new DAL.DapperRepository<DmnTaxSettings>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("TaxSettingsId", objTestClass.TaxSettingsId);
            var lstData = objDapperRepository.Remove(procedureName, parms);
            return lstData;
        }
    }
}
