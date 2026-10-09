using Dapper;
using Domain;

namespace BLL
{
    public class BllReason
    {
        public DmnReason AddeditData(
            string procedureName,
            DmnReason objReason)
        {
            DAL.DapperRepository<DmnReason> repository =
                new DAL.DapperRepository<DmnReason>();

            DynamicParameters parms = new DynamicParameters();

            parms.Add("@ReasonId", objReason.ReasonId);
            parms.Add("@ReasonName", objReason.ReasonName);
            parms.Add("@Description", objReason.Description);
            parms.Add("@BranchId", objReason.BranchId);
            parms.Add("@CreatedBy", objReason.CreatedBy);
            parms.Add("@Status", objReason.Status);

            var result = repository.AddandReturnID(
                procedureName,
                parms);

            return result;
        }
    }
}