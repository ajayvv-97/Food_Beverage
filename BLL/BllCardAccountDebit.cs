
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Domain;
namespace BLL
{
    public class BllCardAccountDebit
    {
        public DmnCardAccountDebit Paymentmidascrad(string sp, DmnCardAccountDebit objectClasss)
        {
            DAL.DapperRepository<DmnCardAccountDebit> objDapperRepository = new DAL.DapperRepository<DmnCardAccountDebit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@DebitLedgerId", objectClasss.DebitLedgerId);
            parms.Add("@DebitICardNo", objectClasss.DebitICardNo);
            parms.Add("@FMCardType", objectClasss.FMCardType);
            parms.Add("@CardType", objectClasss.CardType);
            parms.Add("@DBname", objectClasss.DBname);
            var status = objDapperRepository.AddandReturnID(sp, parms);
            return status;
        }

    }
}
