using Dapper;
using Domain; // for DmnMenu

namespace BLL
{
    public class BllQtyReserve
    {
        public DmnMenu UpdateReserveQty(string procedureName, DmnMenu menu)
        {
            DAL.DapperRepository<DmnMenu> dapperRepository = new DAL.DapperRepository<DmnMenu>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("TableType", menu.TableType);
            parms.Add("ActionType", menu.Actiontype);
            parms.Add("Id", menu.ProductId);
            parms.Add("Qty", menu.OrderQty);
            parms.Add("ProductType", menu.ProductType);
            parms.Add("ReservedQty", menu.OrderReservedQty);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        public DmnMenu CancelReserveQty(string procedureName, DmnMenu menu)
        {
            DAL.DapperRepository<DmnMenu> dapperRepository = new DAL.DapperRepository<DmnMenu>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("TableType", menu.TableType);
            parms.Add("ActionType", 3); // Assuming 3 = cancel action
            parms.Add("Id", menu.ProductId);
            parms.Add("Qty", menu.OrderQty);
            parms.Add("ProductType", menu.ProductType);
            parms.Add("ReservedQty", menu.OrderQty);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }

        public DmnMenu ResetAllReservations(string procedureName, DmnMenu objTestClass)
        {
            DAL.DapperRepository<DmnMenu> dapperRepository = new DAL.DapperRepository<DmnMenu>();
            DynamicParameters parms = new DynamicParameters();

            parms.Add("TableType", objTestClass.TableType);
            parms.Add("ActionType", 3); // Assuming 3 = cancel/reset action
            parms.Add("Id", objTestClass.ProductId);
            parms.Add("Qty", null);
            parms.Add("ProductType", objTestClass.ProductType);
            parms.Add("ReservedQty", 0);

            var lstData = dapperRepository.AddandReturnID(procedureName, parms);
            return lstData;
        }
    }
}
