using API.Utilites;
using ClassLibrary;
using ClassLibrary1;
using FYPAPI.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;

namespace FYPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _service;

        public readonly IWebHostEnvironment _environment;

        public readonly IConfiguration _configuration;


        public CategoryController(ICategoryServices services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _service = services;

            _environment = environment;
            _configuration = configuration;
        }


        [HttpPost("AddUpdatecategory")]
        public Response AddUpdatecategory(Category obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                obj.CreatedBy = user.UserId;


                var res = _service.AddUpdatecategory(obj);
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



        [HttpPost("GetAllCategory")]
        public Response CategoryGetAll()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.GetAllCategory();
                response = CustomStatusResponse.GetResponse(200);
                if (res!=null )
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


        [HttpPost("GetCategoryById/{id}")]
        public Response GetCategoryById(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.GetCategoryById(id);
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






        [HttpPost("InActive/{id}")]
        public Response InActive(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.InActive(id);
                response = CustomStatusResponse.GetResponse(200);
                if (res >0)
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



        [HttpPost("getCategoriesDropDown")]
        public Response getCategoriesDropDown()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.getCategoriesDropDown();
                response = CustomStatusResponse.GetResponse(200);
                if (res !=null)
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





        [HttpPost]
        [Route("ImageUpload")]
        public async Task<Response> FilesAndImageUpload(IList<IFormFile> files, string filetype)
        {
            Response result = null;

            try
            {
                string mypath = @"/Images/Category/";
                foreach (var file in files)
                {
                    var basePath = Path.Combine(_environment.WebRootPath + "\\Images\\Category\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);


                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var filePath = Path.Combine(basePath, file.FileName);
                    var extension = Path.GetExtension(file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    result = new Response
                    {
                        ResponseMsg = "Request Successful!",
                        Status = 200,
                        Data = new
                        {
                            path = _configuration.GetSection("APIURL").Value.ToString() + mypath+fileName+ extension,
                            filetype = filetype
                        },
                        Token = null
                    };



                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
