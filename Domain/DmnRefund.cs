using System;

namespace Domain
{
    public class DmnRefund
    {
        public int Id { get; set; }
        public int OrderMasterId { get; set; }
        public int SalesMasterId { get; set; }

        public string ReasonNote { get; set; }
        public int RefundpayType { get; set; }
        public int RefundReason { get; set; }
        public decimal CashRefund { get; set; }

        public string IcardNo { get; set; }
        public int LedgerId { get; set; }

        public decimal CashBalance { get; set; }
        public decimal BonusBalance { get; set; }
        public decimal PointBalnce { get; set; }
        public decimal TokenBalance { get; set; }

        public decimal CashBalanceRefund { get; set; }
        public decimal BonusRefund { get; set; }
        public decimal PointRefund { get; set; }
        public decimal TokenRefund { get; set; }

        public int Status { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public String DebitICardNo { get; set; }
        public int DebitLedgerId { get; set; }
        public int CardType { get; set; }
        public int FMCardType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public String DBname { get; set; }
        public bool Cancelorderstatus { get; set; }
    }
}
