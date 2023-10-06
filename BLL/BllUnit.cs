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
    }
}