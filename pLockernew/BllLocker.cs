using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using BLL;
using Dapper;

namespace BLL
{
    public class BllLocker
    {
        public bool AddEditData(string procedureName, DmnLocker objTestClass)
        {
            DAL.DapperRepository<DmnLocker> objDapperRepository = new DAL.DapperRepository<DmnLocker>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("LockerID", objTestClass.ID);
            parms.Add("Locker", objTestClass.Locker);
            parms.Add("MacID", objTestClass.MacID);
            parms.Add("RFID", objTestClass.RFID);
            parms.Add("PINno", objTestClass.PinNo);
            parms.Add("LockerRowno", objTestClass.LockerRowno);
            parms.Add("LockerColor", objTestClass.LockerColor);
            parms.Add("HrRate", objTestClass.HrRate);
            parms.Add("DyRate", objTestClass.DyRate);
            parms.Add("MnRate", objTestClass.MnRate);
            parms.Add("LockDesc", objTestClass.LockDesc);
            parms.Add("LocationID", objTestClass.LocationID);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            //parms.Add("CreatedDateTime", objTestClass.CreatedDateTime);
            parms.Add("ModifiedBy", objTestClass.ModifiedBy);
            //parms.Add("ModifiedDateTime", objTestClass.ModifiedDateTime);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);

            var lstData = objDapperRepository.Add(procedureName, parms);
            return lstData;
        }
    }
}
