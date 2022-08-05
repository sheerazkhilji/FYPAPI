using API.DBManager;
using ClassLibrary1;
using Dapper;
using FYPAPI.IServices;
using System.Collections.Generic;
using System.Data;

namespace FYPAPI.Services
{
    public class orderServices : IorderServices
    {


        private readonly IDapper _dapper;
        public orderServices(IDapper dapper)
        {
            _dapper = dapper;
        }

      
        public List<OrderSlip> PlaceOrder(Order obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", obj.ProductsIds, DbType.String, ParameterDirection.Input);
            parameters.Add("@PaymentMethodType", obj.PaymentMethodType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IsShippingDifferentAddress", obj.IsShippingDifferentAddress, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@AdditionalInfo", obj.AdditionalInfo, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserId", obj.UserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Name", obj.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@Mobile", obj.MobileNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@City", obj.CityName, DbType.String, ParameterDirection.Input);
            parameters.Add("Address", obj.Address, DbType.String, ParameterDirection.Input);
            return _dapper.GetAll<OrderSlip>(@"[dbo].[usp_OrderPlace]", parameters);

        }
        public List<OrderUserId> GetOrdersByUser(int id)
        {
              DynamicParameters parameters = new DynamicParameters();
             parameters.Add("@UserId", id, DbType.Int32, ParameterDirection.Input);
          
            return _dapper.GetAll<OrderUserId>(@"[dbo].[usp_GetOrderListByUserId]", parameters);
        }

        public List<OrderUserId> GetOrdersItemsByProductsIds(int id, int orderids)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", id, DbType.Int32, ParameterDirection.Input);
             parameters.Add("@OrderId", orderids, DbType.Int32, ParameterDirection.Input);

            return _dapper.GetAll<OrderUserId>(@"[dbo].[usp_getOrderProdcutsByUserId]", parameters);

        }

        public int ChangeOrderStatus(OrderUserId obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OrderIds", obj.OrderId, DbType.String, ParameterDirection.Input);
            parameters.Add("@Status", obj.OrderStatus, DbType.String, ParameterDirection.Input);
             parameters.Add("@CustomerId", obj.customerId, DbType.Int32, ParameterDirection.Input);
             parameters.Add("@UserId", obj.Vendor, DbType.Int32, ParameterDirection.Input);

            return _dapper.Insert<int>(@"[dbo].[usp_ChangeStatusOrder]", parameters);
        }
        //Test

        public List<OrderSlip> OrderSlip()
        {
            DynamicParameters parameters = new DynamicParameters();
           
            return _dapper.GetAll<OrderSlip>(@"[dbo].[TestSlip]", parameters);
        }

    }
}
