using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
	public class BllPromoMaster
	{
		public DmnPromoMaster AddPromo(string StoredProcedure,DmnPromoMaster dmnPromo)
		{
			DAL.DapperRepository<DmnPromoMaster> dapper = new DAL.DapperRepository<DmnPromoMaster>();
			DynamicParameters parms = new DynamicParameters();
			parms.Add("PromoMId", dmnPromo.PromoMId);
			parms.Add("Caption", dmnPromo.Caption);
			parms.Add("Description", dmnPromo.Description);
			parms.Add("From", dmnPromo.StartDate);
			parms.Add("To", dmnPromo.EndDate);
			parms.Add("MinBillAmt", dmnPromo.MinBillAmt);
			parms.Add("MaxDiscount", dmnPromo.MaxDiscount);
			parms.Add("DiscountPer", dmnPromo.DiscountPer);
			parms.Add("PromoLabel", dmnPromo.PromoLabel);
			parms.Add("TotalPromo", dmnPromo.TotalPromo);
			parms.Add("Status", dmnPromo.Status);
			parms.Add("CreatedBy", dmnPromo.CreatedBy);
			var lstData = dapper.AddandReturnID(StoredProcedure, parms);
			return lstData;
		}
		public bool AddPromoDetails(string StoredProcedure,DmnPromoDetails dmnProDet)
		{
			DAL.DapperRepository<DmnPromoDetails> dapper = new DAL.DapperRepository<DmnPromoDetails>();
			DynamicParameters parms = new DynamicParameters();
			parms.Add("PromoMId", dmnProDet.PromoMId);			
			parms.Add("Code", dmnProDet.Code);			
			parms.Add("Status", dmnProDet.Status);			
			parms.Add("CreatedBy", dmnProDet.CreatedBy);			
			bool lstData = dapper.Add(StoredProcedure, parms);
			return lstData;
		}
	}
}
