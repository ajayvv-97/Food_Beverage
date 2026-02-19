using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllPartyPack
    {
        public DmnPartyPack AddEditData(string procedureName, DmnPartyPack obj)
        {
            DAL.DapperRepository<DmnPartyPack> repo = new DAL.DapperRepository<DmnPartyPack>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("PartyPackId", obj.PartyPackId);
            parms.Add("PackName", obj.PackName);
            parms.Add("Price", obj.Price);
            parms.Add("DiscountPer", obj.DiscountPer);
            parms.Add("DiscountAmt", obj.DiscountAmt);
            parms.Add("TaxType", obj.TaxType);
            parms.Add("TaxSettingsId", obj.TaxSettingsId);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("BranchId", obj.BranchId);         

            var data = repo.AddandReturnID(procedureName, parms);
            return data;
        }
        
            public DmnPartyPackDetails AddData_packdetails(string procedureName, DmnPartyPackDetails obj)
            {
                DAL.DapperRepository<DmnPartyPackDetails> repo =
                    new DAL.DapperRepository<DmnPartyPackDetails>();

                DynamicParameters parms = new DynamicParameters();

                parms.Add("PartyPackDetailsId", obj.PartyPackDetailsId);
                parms.Add("PartyPackId", obj.PartyPackId);
                parms.Add("Item", obj.Item);
                parms.Add("Quantity", obj.Quantity);
                parms.Add("CreatedBy", obj.CreatedBy);
                parms.Add("BranchId", obj.BranchId);
                parms.Add("Status", obj.Status);

                var data = repo.AddandReturnID(procedureName, parms);
                return data;
            }
        

    }

}
