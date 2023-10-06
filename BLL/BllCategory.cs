
using Dapper;
using Domain;

namespace BLL
{
    public class BllCategory
    {
        public DmnCategory AddeditData(string procedureName, DmnCategory objTestClass)
        {
            DAL.DapperRepository<DmnCategory> objDapperRepository = new DAL.DapperRepository<DmnCategory>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Fcid", objTestClass.Fcid);
            parms.Add("CategoryName", objTestClass.CategoryName);
            parms.Add("CategoryType", objTestClass.CategoryType);            
            parms.Add("Description", objTestClass.Description);
            parms.Add("BranchId", objTestClass.BranchId); ;
            parms.Add("CreatedBy", objTestClass.CreatedBy);
            parms.Add("Status", objTestClass.Status);
            var lstData = objDapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}