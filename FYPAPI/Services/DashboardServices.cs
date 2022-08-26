using API.DBManager;
using ClassLibrary1;
using Dapper;
using FYPAPI.IServices;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FYPAPI.Services
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IDapper _dapper;
        public DashboardServices(IDapper dapper)
        {
            _dapper=dapper;
        }
        public DashBoardModel GetdashboardDate()
        {
            DashBoardModel dashBoardModel=new DashBoardModel();
            List<CustomerVendorTrafic> trafic = new List<CustomerVendorTrafic>();


            DynamicParameters parameters = new DynamicParameters();

            var data = _dapper.GetMultipleObjects(@"[dbo].[Usp_GetAdminDashBordStatus]", parameters, gr => gr.Read<int>(), gr => gr.Read<int>(), gr => gr.Read<int>(), gr => gr.Read<int>(), gr => gr.Read<int>(), gr => gr.Read<DBProduct>(), gr => gr.Read<DBVendor>(), gr => gr.Read<DBCustomers>(), gr => gr.Read<DBOrderStatus>(), gr => gr.Read<int>());


            dashBoardModel.TotalVendors = data.Item1.FirstOrDefault();
            dashBoardModel.TotalEarned = data.Item2.FirstOrDefault();
            dashBoardModel.InventoryStokes = data.Item3.FirstOrDefault();
            dashBoardModel.TotalOrdersCompleted = data.Item4.FirstOrDefault();
            dashBoardModel.TotalCustomerCount = data.Item5.FirstOrDefault();
            dashBoardModel.products = data.Item6.ToList();
            dashBoardModel.vendor = data.Item7.Item1.FirstOrDefault();
            dashBoardModel.Customers = data.Item7.Item2.FirstOrDefault();
            dashBoardModel.orderStatus = data.Item7.Item3.ToList();


            dashBoardModel.Revenue = data.Item7.Item4.FirstOrDefault();





            return dashBoardModel;




        }

        public List<OrderUserId> GetOrdersList()
        {
            DynamicParameters parameters = new DynamicParameters();
          
            return _dapper.GetAll<OrderUserId>(@"[dbo].[usp_Admin_GetOrderList]", parameters);
        }

        public List<OrderUserId> GetOrdersItemsByProductsIds(RequestParameters obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@StoreName", obj.StoreName, DbType.String, ParameterDirection.Input);
            parameters.Add("@OrderId", obj.OrderId, DbType.Int32, ParameterDirection.Input);

            return _dapper.GetAll<OrderUserId>(@"[dbo].[usp_Admin_getOrderProdcutsByUserId]", parameters);

        }
    }
}
