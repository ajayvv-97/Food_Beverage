using Dapper;
using Domain;

namespace BLL
{
    public class BllPartyBookingPayment
    {
        public DmnPartyBookingPayment AddEditPayment(DmnPartyBookingPayment obj)
        {
            DAL.DapperRepository<DmnPartyBookingPayment> repo = new DAL.DapperRepository<DmnPartyBookingPayment>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("PaymentId", obj.PaymentId);
            parms.Add("PartyBookingId", obj.PartyBookingId);
            parms.Add("PaymentMode", obj.PaymentMode);
            parms.Add("PaymentAmount", obj.PaymentAmount);

            // 🔹 New fields
            parms.Add("RcvAmnt", obj.RcvAmnt);
            parms.Add("BalAmnt", obj.BalAmnt);

            parms.Add("IsAdvance", obj.IsAdvance);
            parms.Add("ReferenceNo", obj.ReferenceNo);
            parms.Add("PaymentDescription", obj.PaymentDescription);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("Status", obj.Status);

            var data = repo.AddandReturnID("prdInsert_Upd_PartyBookingPayment", parms);
            return data;
        }
    }
}