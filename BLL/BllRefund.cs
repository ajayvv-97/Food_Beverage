
using Dapper;
using Domain;
using System;

namespace BLL
{
    public class BllRefund
    {
        public DmnRefund AddRefund(string procedureName, DmnRefund dmnRefund)
        {
            DAL.DapperRepository<DmnRefund> repo = new DAL.DapperRepository<DmnRefund>();
            DynamicParameters parms = new DynamicParameters();

            // Mandatory IDs
            parms.Add("@OrderMasterId", dmnRefund.OrderMasterId);
            parms.Add("@SalesMasterId", dmnRefund.SalesMasterId);

            // Reason details
            parms.Add("@ReasonNote", dmnRefund.ReasonNote);
            parms.Add("@RefundType", dmnRefund.RefundpayType);
            parms.Add("@RefundReason", dmnRefund.RefundReason);
            parms.Add("@CashRefund", dmnRefund.CashRefund);
            parms.Add("@CashBalanceRefund", dmnRefund.CashBalanceRefund);
            parms.Add("@DebitICardNo", dmnRefund.DebitICardNo);
            parms.Add("@DebitLedgerId", dmnRefund.DebitLedgerId);
            parms.Add("@FMCardType", dmnRefund.FMCardType);
            parms.Add("@CardType", dmnRefund.CardType);

            //// Card details
            //parms.Add("@IcardNo", dmnRefund.IcardNo);
            //parms.Add("@LedgerId", dmnRefund.LedgerId);

            //// Balances
            //parms.Add("@CashBalance", dmnRefund.CashBalance);
            //parms.Add("@BonusBalance", dmnRefund.BonusBalance);
            //parms.Add("@PointBalnce", dmnRefund.PointBalnce);
            //parms.Add("@TokenBalance", dmnRefund.TokenBalance);

            //// Refund amounts
            //parms.Add("@CashBalanceRefund", dmnRefund.CashBalanceRefund);
            //parms.Add("@BonusRefund", dmnRefund.BonusRefund);
            //parms.Add("@PointRefund", dmnRefund.PointRefund);
            //parms.Add("@TokenRefund", dmnRefund.TokenRefund);

            //// Other fields

            
            parms.Add("@DBname", dmnRefund.DBname);
            parms.Add("@Status", dmnRefund.Status);
            parms.Add("@BranchId", dmnRefund.BranchId);
            parms.Add("@CreatedBy", dmnRefund.CreatedBy);
            var result = repo.AddandReturnID(procedureName, parms);
            return result;
        }
    }
}
