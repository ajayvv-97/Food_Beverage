using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Bllpayment
    {
        public DmnPayment AddEditPayment(string procedureName, DmnPayment payment)
        {
            DAL.DapperRepository<DmnPayment> dapperRepository = new DAL.DapperRepository<DmnPayment>();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("PayId", payment.PayId);
            parameters.Add("OrderMasterID", payment.OrderMasterID);
            parameters.Add("PayType", payment.PayType);
            parameters.Add("Amount", payment.Amount);
            parameters.Add("Tid", payment.Tid);
            parameters.Add("SalemasterId", payment.SalemasterId);
            parameters.Add("BranchId", payment.BranchId);
            parameters.Add("CreatedBy", payment.CreatedBy);
            parameters.Add("Status", payment.Status);
            parameters.Add("OrderDetailsId", payment.OrderDetailsId);
            parameters.Add("SplitType", payment.SplitType);
            parameters.Add("BillCount", payment.BillCount);

            parameters.Add("OaAmount", payment.OaAmount);
            parameters.Add("OdAmount", payment.OdAmount);
            parameters.Add("PromoAmt", payment.PromoAmt);
            parameters.Add("DiscountAmount", payment.DiscountAmount);
            parameters.Add("NumberCount", payment.NumberCount);

            parameters.Add("BalAmnt", payment.BalAmnt);
            parameters.Add("RcvAmnt", payment.RcvAmnt);

            var lstData = dapperRepository.AddandReturnID(procedureName, parameters);
            return lstData;
        }
      
    }
}