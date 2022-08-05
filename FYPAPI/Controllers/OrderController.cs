using API.Utilites;
using ClassLibrary;
using ClassLibrary1;
using FYPAPI.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace FYPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IorderServices _services;
        public OrderController(IorderServices services)
        {
            _services=services;
        }

        [HttpPost("PlaceOrder")]
        public Response PlaceOrder(Order obj)
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





        [HttpPost("PrintOrderSlip")]
        public Response PrintOrderSlip()
        {
            UserManagement user = null;
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
