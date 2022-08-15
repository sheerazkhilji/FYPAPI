using API.Utilites;
using ClassLibrary;
using FYPAPI.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Common;

namespace FYPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashBoardController : ControllerBase
    {
        public readonly IDashboardServices _services;
        public AdminDashBoardController(IDashboardServices services)
        {
            _services = services;

            
        }




        [HttpPost("GetdashboardDate")]
        public Response GetdashboardDate()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _services.GetdashboardDate();

                
                
                
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
