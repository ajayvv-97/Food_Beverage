using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllGameOffer
    {
        public bool AddOffer(string procedureName, DmnGameOffer objtestclass)
        {
            DAL.DapperRepository<DmnHappyHours> objDapperRepository = new DAL.DapperRepository<DmnHappyHours>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@fDt", objtestclass.fDt);
            parms.Add("@tDt", objtestclass.tDt);
            parms.Add("@OfferDesc", objtestclass.OfferDesc);
            parms.Add("@gmId", objtestclass.gmid);
            parms.Add("@OffrRate", objtestclass.offerRate);
            parms.Add("@DiscRate", objtestclass.DiscRate);
            parms.Add("@DiscPer", objtestclass.DiscPer);
            parms.Add("@VIPOffrRate", objtestclass.VIPOfferRate);
            parms.Add("@VIPDiscRate", objtestclass.VIPDiscRate);
            parms.Add("@VIPDiscPer", objtestclass.VIPDiscPer);
            parms.Add("@CreatedBy", objtestclass.CreatedBy);
            parms.Add("@Status", objtestclass.status);
            parms.Add("@BranchId", objtestclass.BranchId);
            parms.Add("@Remark", objtestclass.Remark);
            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
    }
}
