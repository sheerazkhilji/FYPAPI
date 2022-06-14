using API.IServices;
using API.Utilites;
using ClassLibrary;
using FYPAPI.Utilites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorServices _service;

        public readonly IWebHostEnvironment _environment;

        public readonly IConfiguration _configuration;


        public VendorController(IVendorServices services,IWebHostEnvironment environment ,IConfiguration configuration)
        {
            _service = services;
        
            _environment = environment;
            _configuration = configuration;
        }




        [HttpPost("SelfVendorRegistration")]
        public Response SelfVendorRegistration(Vendor obj)
        {
           
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(obj.PorfileImgPath))
                {
                    byte[] bytes = Convert.FromBase64String(obj.PorfileImgPath);
                    string filename = ImageManager.AppendTimeStampForFiles(obj.UserEmail, "VendorProfiles." + obj.FileExtension);
                    string path = @"wwwroot/VendorProfiles/";
                    string filePath = path + filename;
                    if (!Directory.Exists(_environment.WebRootPath + "\\VendorProfiles\\"))
                        Directory.CreateDirectory(_environment.WebRootPath + "\\VendorProfiles\\");
                    FileStream stream = new FileStream(@filePath, FileMode.CreateNew);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                    obj.PorfileImgPath = ((filename != "") ? (_configuration.GetSection("APIURL").Value.ToString() + filePath) : "");
                }


                if (!string.IsNullOrEmpty(obj.StoreImage))
                {
                    byte[] bytes = Convert.FromBase64String(obj.StoreImage);
                    string filename = ImageManager.AppendTimeStampForFiles(obj.UserEmail, "StoreImage." + obj.FileExtension);
                    string path = @"wwwroot/VendorProfiles/";
                    string filePath = path + filename;
                    if (!Directory.Exists(_environment.WebRootPath + "\\StoreImage\\"))
                        Directory.CreateDirectory(_environment.WebRootPath + "\\StoreImage\\");
                    FileStream stream = new FileStream(@filePath, FileMode.CreateNew);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Close();
                    obj.StoreImage = ((filename != "") ? (_configuration.GetSection("APIURL").Value.ToString() + filePath) : "");
                }


                var res = _service.SelfVendorRegistration(obj);
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



        [HttpPost("getAllVendors")]
        public Response getAllVendors()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.getAllVendors();
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


        [HttpPost("getAllVendorsForApprovals")]
        public Response getAllVendorsForApprovals()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.getAllVendorsForApprovals();
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



        [HttpPost("getAllVendors/{id}")]
        public Response getAllVendors(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.GetVendorById(id);
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




        [HttpPost("Approve/{id}")]
        public Response usp_Approve(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.Approve(id);
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




        [HttpPost("DisApprove/{id}")]
        public Response DisApprove(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.DisApprove(id);
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


        [HttpPost("DeleteVendor/{id}")]
        public Response DeleteVendor(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.DeleteVendor(id);
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









    }
}
