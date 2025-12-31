using Dapper;
using Domain;

namespace BLL
{
    public class BllUnit
    {
        public DmnUnit AddeditData(string procedureName, DmnUnit objTestClass)
        {
            DAL.DapperRepository<DmnUnit> objDapperRepository = new DAL.DapperRepository<DmnUnit>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("UId", objTestClass.UId);
            parms.Add("UnitName", objTestClass.UnitName);
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
        public DmnUnitConversion AddeditConversion(string procedureName, DmnUnitConversion objTestClass)
        {
            DAL.DapperRepository<DmnUnitConversion> objDapperRepository = new DAL.DapperRepository<DmnUnitConversion>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("id", objTestClass.id);
            parms.Add("FromUnit", objTestClass.FromUnit);
            parms.Add("ToUnit", objTestClass.ToUnit);
            parms.Add("Factor", objTestClass.Factor); ;
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}