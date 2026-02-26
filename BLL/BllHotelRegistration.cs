using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;
namespace BLL
{
    public class BllHotelRegistration
    {
        public DmnHRegistration AddEditData(string procedureName, DmnHRegistration objTestClass)
        {
            DAL.DapperRepository<DmnHRegistration> objDapperRepository = new DAL.DapperRepository<DmnHRegistration>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Id", objTestClass.Id);
            parms.Add("Name", objTestClass.Name);
            parms.Add("Address", objTestClass.Address);
            parms.Add("Phone", objTestClass.Phone);
            parms.Add("GstNo", objTestClass.GstNo);
            parms.Add("ZipCode", objTestClass.ZipCode);
            parms.Add("Certificate", objTestClass.Certificate);
            parms.Add("Country", objTestClass.Country);
            parms.Add("City", objTestClass.City);
            parms.Add("State", objTestClass.State);
            parms.Add("Email", objTestClass.Email);
            parms.Add("Photo", objTestClass.Photo);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("BranchId", objTestClass.BranchId);
            parms.Add("Status", objTestClass.Status);
            parms.Add("Certificate1", objTestClass.Certificate1);
            parms.Add("Certificate2", objTestClass.Certificate2);
            parms.Add("BookingInerwell", objTestClass.BookingInerwell);
            parms.Add("website", objTestClass.website); 
            parms.Add("billno", objTestClass.billno);
            parms.Add("GameBranchId", objTestClass.GameBranchId);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public bool AddEditWorkTime(string procedurename, DmnLocationWorkTime objTestClass)
        {
            DAL.DapperRepository<DmnLocationWorkTime> dapperRepository = new DAL.DapperRepository<DmnLocationWorkTime>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("LocId", objTestClass.LocId);
            parms.Add("DayId", objTestClass.DayId);
            parms.Add("FromTime", objTestClass.FromTime);
            parms.Add("ToTime", objTestClass.ToTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            bool data = dapperRepository.Add(procedurename, parms);
            return data;
        }

    }
}


