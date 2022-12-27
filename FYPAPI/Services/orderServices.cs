using API.DBManager;
using ClassLibrary1;
using Dapper;
using FYPAPI.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
            var table1 = new DataTable();
            table1.Columns.Add("Color", typeof(string));
            table1.Columns.Add("ProductId", typeof(string));
       

            foreach (var item in obj.productsColors)
            {
                var row = table1.NewRow();
                row["Color"] = Convert.ToInt32(item.color);
                row["ProductId"] = Convert.ToInt32(item.productid);
              

                table1.Rows.Add(row);
            }

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

            parameters.Add("@type_PurchedProductColors", table1.AsTableValuedParameter("dbo.type_PurchedProductColors"));
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

        public List<PurchedModelColor> GetPurchedModelByOrderAndProductId(Parameter obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", obj.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@OrderId", obj.OrderId, DbType.Int32, ParameterDirection.Input);

            return _dapper.GetAll<PurchedModelColor>(@"[dbo].[usp_GetPurchedModelByOrderAndProductId]", parameters);

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



        public Ordertracking Ordertracking(string ordernumber)
        {
            Ordertracking ordertracking = new Ordertracking();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ordernumber", ordernumber, DbType.String, ParameterDirection.Input);

            var data = _dapper.GetMultipleObjects(@"[dbo].[usp_OrderTracking]", parameters, gr => gr.Read<Order>(), gr => gr.Read<OrderSlip>(), gr => gr.Read<OrderSlip>(), gr => gr.Read<OrderSlip>());

            ordertracking.order = data.Item1.FirstOrDefault();
            ordertracking.CustomerOrderItems = data.Item2.ToList();
            ordertracking.OrderNeedtoBeDelivered = data.Item3.ToList();
            ordertracking.OrderNeedNottoBeDelivered = data.Item4.ToList();


            return ordertracking;
        }


        //Test

        public List<OrderSlip> OrderSlip()
        {
            DynamicParameters parameters = new DynamicParameters();
           
            return _dapper.GetAll<OrderSlip>(@"[dbo].[TestSlip]", parameters);
        }

    }
}
