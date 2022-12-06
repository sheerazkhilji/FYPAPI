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
using System.Text.Json;
using System.Threading.Tasks;

namespace FYPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IWebHostEnvironment _environment;

        public readonly IConfiguration _configuration;
        public readonly IProductServices _service;


        public ProductsController(IProductServices services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _service = services;

            _environment = environment;
            _configuration = configuration;
        }




        [HttpPost("AddUpdateproduct")]
        public Response AddUpdateproduct(Product obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                obj.CreatedBy = user.UserId;


                var res = _service.AddUpdateproduct(obj);
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





        [HttpPost("GetAllProduct")]
        public Response GetAllProduct()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                int userId = user.UserId;

                var res = _service.GetAllProduct(userId);
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




        [HttpPost("GetProductById/{id}")]
        public Response GetProductById(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

              

                var res = _service.GetProductById(id);
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
        
        [HttpPost("GetProductByIdForCustomer/{id}")]
        public Response GetProductByIdForCustomer(int id)
        {
            Response response = new Response();
            try
            {

              

                var res = _service.GetProductById(id);
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
                response.ResponseMsg = ex.Message;
                return response;
            }

        }


        [HttpPost("GetExclusiveProducts")]
        public Response GetExclusiveProducts()
        {
          
            Response response = new Response();
            try
            {

                var res = _service.GetExclusiveProducts();
              
                for (int i = 0; i < res.Count; i++)
                {
                    if (res[i].ProductRatingPer!= "[{}]")
                    {
                        var rc = JsonSerializer.Deserialize<List<DeserializeProductrating>>(res[i].ProductRatingPer);
                        res[i].ProductRating = rc[0].NumberOfStars;

                    }



                }





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


      




        [HttpPost("GetCategories")]
        public Response GetCategories()
        {

            Response response = new Response();
            try
            {

                var res = _service.GetCategories();
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



        [HttpPost("GetShopDate")]
        public Response GetShopDate()
        {

            Response response = new Response();
            try
            {

                var res = _service.GetShopDate();
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


        [HttpPost]
        [Route("ImageUpload")]
        public async Task<Response> FilesAndImageUpload(IList<IFormFile> files, string filetype)
        {
            Response result = null;

            try
            {
                string mypath = @"/Images/Product/";
                foreach (var file in files)
                {
                    var basePath = Path.Combine(_environment.WebRootPath + "\\Images\\Product\\");
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
                            path = _configuration.GetSection("APIURL").Value.ToString() + mypath + fileName + extension,
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


        [HttpPost]
        [Route("ModelUpload")]
        public async Task<Response> ModelUpload(IList<IFormFile> files, string filetype)
        {
            Response result = null;

            try
            {
                string mypath = @"/ProductModels/";
                foreach (var file in files)
                {
                    var basePath = Path.Combine(_environment.WebRootPath + "\\ProductModels\\");
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
                            path = _configuration.GetSection("APIURL").Value.ToString() + mypath + fileName + extension,
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

        [HttpPost("GetColorsAndLayernameByID")]
        public Response GetColorsAndLayernameByID(int id)
        {
            Response response = new Response();
            try
            {

                var res = _service.GetColorsAndLayernameByID(id);
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


        [HttpPost("AddProductsReviews")]
        public Response AddProductsReviews(ProductsReviews obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                obj.UserId = user.UserId;

                var res = _service.AddReview(obj);
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






    }
}
