using API.Utilites;
using ClassLibrary;
using ClassLibrary1;
using FYPAPI.IServices;
using FYPAPI.PaymentHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace FYPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IorderServices _services;

        private readonly IStripeAppService _stripeService;
        public OrderController(IorderServices services, IStripeAppService stripeService)
        {
            _services = services;
            _stripeService = stripeService; 
        }

        [HttpPost("PlaceOrderOnCash")]
        public Response PlaceOrderOnCash(Order obj)
        {
            UserManagement user = null;
            Response response = new Response();

            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                obj.UserId = user.UserId;

              



                var res = _services.PlaceOrder(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res!=null)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }


       
      
    


    [HttpPost("customer/add")]
        public async Task<object> AddStripeCustomer( [FromBody] AddStripeCustomer customer, CancellationToken ct)
        {

           
            UserManagement user = null;
            Response response = new Response();
            try
            {

                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer,ct);

            if (StatusCodes.Status200OK==200)
            {



                PaymentMethod pay = new PaymentMethod(_stripeService);



                Order orderObj = new Order();

                    orderObj.OrderId = customer.OrderId;
                    orderObj.ProductsIds= customer.ProductsIds;
                    orderObj.PaymentMethodType= 2;
                    orderObj.IsShippingDifferentAddress= customer.IsShippingDifferentAddress;
                    orderObj.OrderStatus= customer.OrderStatus;
                    orderObj.OrderNumber= customer.OrderNumber;
                    orderObj.Deliverytime= customer.Deliverytime;
                    orderObj.orderdate= customer.orderdate;
                    orderObj.AdditionalInfo= customer.AdditionalInfo;
                    orderObj.UserId = user.UserId;
                    orderObj.Name= customer.Name;
                    orderObj.MobileNumber= customer.MobileNumber;
                    orderObj.CityName= customer.CityName;
                    orderObj.Address= customer.Address;
                    orderObj.productsColors= customer.productsColors;

                    var  res = _services.PlaceOrder(orderObj);

                    int totlalPrice=0;

                    for (int i = 0; i < res.Count; i++)
                    {
                        totlalPrice += (int)res[i].TotalPrice;
                    }

                    AddStripePaymentclass a = new AddStripePaymentclass(createdCustomer.CustomerId, createdCustomer.Email, "Test", "USD", totlalPrice*100);


                    object stripeObj = await pay.AddStripePayment(a, ct);

                    response = CustomStatusResponse.GetResponse(200);
                    if (res != null)
                    {
                        response.Data = res;
                    }
                    return response;
                 



                }

             
         
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }



        }

        [HttpPost("GetOrdersByUserId")]
        public Response GetOrdersByUser()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
               int id= user.UserId;


                var res = _services.GetOrdersByUser(id);
                response = CustomStatusResponse.GetResponse(200);
                if (res != null)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }
     
        
        
        
        [HttpPost("GetOrdersItemsByProductsIds/{id}")]
        public Response GetOrdersItemsByProductsIds(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                int userid = user.UserId;


                var res = _services.GetOrdersItemsByProductsIds(userid, id);
                response = CustomStatusResponse.GetResponse(200);
                if (res != null)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }

        [HttpPost("GetPurchedModelByOrderAndProductId")]
        public Response GetPurchedModelByOrderAndProductId(Parameter obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                int userid = user.UserId;


                var res = _services.GetPurchedModelByOrderAndProductId(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res != null)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }






        [HttpPost("ChangeOrderStatus")]
        public Response ChangeOrderStatus(OrderUserId obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                int userid = user.UserId;
                obj.Vendor = userid;

                var res = _services.ChangeOrderStatus(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res > 0)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }

        [HttpPost("Ordertracking")]
        public Response Ordertracking(Order obj)
        {
          
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
             
                var res = _services.Ordertracking(obj.OrderNumber);
                response = CustomStatusResponse.GetResponse(200);
                if (res != null)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }




        [HttpPost("PrintOrderSlip")]
        public Response PrintOrderSlip()
        {
          //  UserManagement user = null;
            Response response = new Response();
            try
            {
                //user = TokenManager.GetValidateToken(Request);
                //if (user == null) return CustomStatusResponse.GetResponse(401);
                //int userid = user.UserId;


                





                var res = _services.OrderSlip();
                response = CustomStatusResponse.GetResponse(200);
                if (res!=null)
                {
                    response.Data = res;
                }
                return response;
            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return response;
            }

        }





    


    }
}
