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

            // 🔹 Sales Refund IDs
            parms.Add("@OrderMasterId", dmnRefund.OrderMasterId);
            parms.Add("@SalesMasterId", dmnRefund.SalesMasterId);

            // 🔹 Party Booking Refund
            parms.Add("@PartyBookingId", dmnRefund.PartyBookingId);
            parms.Add("@RefundAmount", dmnRefund.RefundAmount);
            parms.Add("@RefundMode", dmnRefund.RefundMode);

            // 🔹 Reason details
            parms.Add("@ReasonNote", dmnRefund.ReasonNote);
            parms.Add("@RefundType", dmnRefund.RefundpayType);
            parms.Add("@RefundReason", dmnRefund.RefundReason);

            // 🔹 Refund values
            parms.Add("@CashRefund", dmnRefund.CashRefund);
            parms.Add("@CashBalanceRefund", dmnRefund.CashBalanceRefund);

            // 🔹 Card refund details
            parms.Add("@DebitICardNo", dmnRefund.DebitICardNo);
            parms.Add("@DebitLedgerId", dmnRefund.DebitLedgerId);
            parms.Add("@FMCardType", dmnRefund.FMCardType);
            parms.Add("@CardType", dmnRefund.CardType);

            // 🔹 System fields
            parms.Add("@DBname", dmnRefund.DBname);
            parms.Add("@Status", dmnRefund.Status);
            parms.Add("@BranchId", dmnRefund.BranchId);
            parms.Add("@CreatedBy", dmnRefund.CreatedBy);
            parms.Add("@Cancelorderstatus", dmnRefund.Cancelorderstatus);

            var result = repo.AddandReturnID(procedureName, parms);

            return result;


            // ============================================
            // OLD CODE (KEPT FOR FUTURE USE)
            // ============================================

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

        }

        public DmnRefund AddRefundBooking(string procedureName, DmnRefund dmnRefund)
        {
            DAL.DapperRepository<DmnRefund> repo = new DAL.DapperRepository<DmnRefund>();
            DynamicParameters parms = new DynamicParameters();

            // 🔹 Party Booking Refund
            parms.Add("@PartyBookingId", dmnRefund.PartyBookingId);
            parms.Add("@RefundAmount", dmnRefund.RefundAmount);
            parms.Add("@RefundMode", dmnRefund.RefundMode);

            // 🔹 Reason
            parms.Add("@ReasonNote", dmnRefund.ReasonNote);
            parms.Add("@RefundReason", dmnRefund.RefundReason);

            // 🔹 System Fields
            parms.Add("@BranchId", dmnRefund.BranchId);
            parms.Add("@CreatedBy", dmnRefund.CreatedBy);
            parms.Add("@SalesMasterId", dmnRefund.SalesMasterId);
            
            var result = repo.AddandReturnID(procedureName, parms);

            return result;
        }
    }
}