using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class BllTableinfo
    {
        public DmnTableinfo AddData(string procedureName, DmnTableinfo objTestClass)
        {
            DAL.DapperRepository<DmnTableinfo> objDapperRepository = new DAL.DapperRepository<DmnTableinfo>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Tid", objTestClass.Tid);
            parms.Add("TableCode", objTestClass.TableCode);
            parms.Add("Capcity", objTestClass.Capcity);
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("RevStatus", objTestClass.RevStatus);
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            parms.Add("OrderStatus", objTestClass.OrderStatus);
            parms.Add("Price", objTestClass.Price);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}