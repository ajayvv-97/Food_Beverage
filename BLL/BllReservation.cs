using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllReservation
    {
        public DmnReservation AddData(string procedureName, DmnReservation objTestClass)
        {
            DAL.DapperRepository<DmnReservation> objDapperRepository = new DAL.DapperRepository<DmnReservation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BookmasterId", objTestClass.BookmasterId);
            parms.Add("cusName", objTestClass.cusName);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Address", objTestClass.Address);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Gender", objTestClass.gender);
            parms.Add("BookingType", objTestClass.BookingType);
            parms.Add("NoOfPeople", objTestClass.NoOfPeople);
            parms.Add("BookingDateTime", objTestClass.BookingDateTime);
            parms.Add("ReservationTotal", objTestClass.ReservationTotal);
            parms.Add("MenuTotal", objTestClass.MenuTotal);
            parms.Add("GrandTotal", objTestClass.GrandTotal);
            parms.Add("DiscountAmt", objTestClass.DiscountAmt);
            parms.Add("DiscountPer", objTestClass.DiscountPer);
            parms.Add("OtherAdd", objTestClass.OtherAdd);
            parms.Add("OtherDed", objTestClass.OtherDed);
            parms.Add("OtherAddText", objTestClass.OtherAddText);
            parms.Add("OtherDedText", objTestClass.OtherDedText);
            parms.Add("NetAmount", objTestClass.NetAmount);
            parms.Add("Terms", objTestClass.Terms);

            parms.Add("AdvancePay", objTestClass.AdvancePay);
            parms.Add("CashRev", objTestClass.CashRev);
            parms.Add("BalnceAmt", objTestClass.BalnceAmt);

            parms.Add("FinalAmount", objTestClass.FinalAmount);
           


            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public bool AddDetails(string procedureName, DmnReservationDetails objTestClass)
        {
            DAL.DapperRepository<DmnReservationDetails> objDapperRepository = new DAL.DapperRepository<DmnReservationDetails>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BookdetailId", objTestClass.BookdetailId);
            parms.Add("BookmasterId", objTestClass.BookmasterId);
            parms.Add("Name", objTestClass.Name);
            parms.Add("Quantity", objTestClass.Quantity);
            parms.Add("Description", objTestClass.Description);
            parms.Add("Price", objTestClass.Price);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("ModifiedBy", objTestClass.modifiedby);
            parms.Add("Rid", objTestClass.Rid);
            parms.Add("MTID", objTestClass.MTID);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Net", objTestClass.Net);
            //parms.Add("RevStatus", objTestClass.RevStatus);
            bool lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
        public bool UpdateData(string procedureName, DmnReservation objTestClass)
        {
            DAL.DapperRepository<DmnReservation> objDapperRepository = new DAL.DapperRepository<DmnReservation>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("CustName", objTestClass.cusName);
            parms.Add("Mobile", objTestClass.Mobile);
            parms.Add("Address", objTestClass.Address);
            parms.Add("EmailId", objTestClass.EmailId);
            parms.Add("Remarks", objTestClass.Remarks);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Gender", objTestClass.gender);
            parms.Add("BookingType", objTestClass.BookingType);
            parms.Add("NoOfPeople", objTestClass.NoOfPeople);
            parms.Add("BookingDateTime", objTestClass.BookingDateTime);
            parms.Add("RevStatus", objTestClass.RevStatus);

            var lstData = objDapperRepository.Update(procedureName, parms);
            return lstData;
        }
    }
}
