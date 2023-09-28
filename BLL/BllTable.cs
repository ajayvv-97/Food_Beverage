using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Dapper;
namespace BLL
{
    public class BllTable
    {
        public Dmntable AddEditData(string procedureName, Dmntable objTestClass)
        {
            DAL.DapperRepository<Dmntable> objDapperRepository = new DAL.DapperRepository<Dmntable>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("TableCode", objTestClass.TableCode);
            parms.Add("Capcity", objTestClass.Capcity);
            parms.Add("TCid", objTestClass.TCid);
            parms.Add("RevStatus", objTestClass.RevStatus);
            parms.Add("Decription", objTestClass.Decription);
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("BranchId", objTestClass.BranchId);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
