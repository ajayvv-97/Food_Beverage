using Dapper;
using Domain;
using System;

namespace BLL
{
    public class BllImageUpload
    {
        public DmnImageUpload UploadPhoto(string procedureName, DmnImageUpload obj)
        {
            var repo = new DAL.DapperRepository<DmnImageUpload>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("PpId", obj.PpId);
            parms.Add("ProductId", obj.ProductId);
            parms.Add("Photo", obj.Photo);
            parms.Add("BranchId", obj.BranchId);
            parms.Add("CreatedBy", obj.CreatedBy);
            parms.Add("Status", obj.Status);
            parms.Add("Width", obj.Width);
            parms.Add("Height", obj.Height);
            parms.Add("Inseration_Type", obj.Inseration_Type);
            return repo.AddandReturnID(procedureName, parms);
        }

        public bool ImgStatusUpdate(string procedureName, DmnImageUpload obj)
        {
            var repo = new DAL.DapperRepository<DmnImageUpload>();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("PpId", obj.PpId);
            parms.Add("Status", obj.Status);
            parms.Add("ModifiedBy", obj.ModifiedBy);
            return repo.Add(procedureName, parms);
        }
    }
}
