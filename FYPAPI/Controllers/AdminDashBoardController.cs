using API.Utilites;
using ClassLibrary;
using ClassLibrary1;
using FYPAPI.IServices;
using FYPAPI.PaymentHelper;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FYPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashBoardController : ControllerBase
    {
        public readonly IDashboardServices _services;

        public readonly IWebHostEnvironment _environment;

        public readonly IConfiguration _configuration;

        private readonly IStripeAppService _stripeService;

        public AdminDashBoardController(IDashboardServices services, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _services = services;

            _environment = environment;
            _configuration = configuration;

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




        [HttpPost("GetOrdersList")]
        public Response GetOrdersList()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _services.GetOrdersList();




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



        [HttpPost("GetOrdersItemsByProductsIds")]
        public Response GetOrdersItemsByProductsIds(RequestParameters obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);
                

                var res = _services.GetOrdersItemsByProductsIds(obj);
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


        [HttpPost("ProductModelServices")]
        public async Task<Response> ProductModelServices(IList<IFormFile> files)
        {
            

            
            UserManagement user = null;
            Response response = null;
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);





                CancellationToken ct = new CancellationToken();

                CreditCard obj = new CreditCard();

                obj.ExpirationYear = Request.Form["expiryDate"].FirstOrDefault().ToString().Split(",")[0];
                obj.ExpirationMonth = Request.Form["expiryDate"].FirstOrDefault().ToString().Split(",")[1];
                obj.CardNumber = Request.Form["cardNumber"].FirstOrDefault();
                obj.CVC = Request.Form["cvc"].FirstOrDefault();
                



            //    AddStripeCardForModelService objs = new AddStripeCardForModelService(Request.Form["nameOnCard"], Request.Form["emailCC"], obj);

              //  StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsyncformodelservice(objs, ct);


                ProductModelServices modelServices = new ProductModelServices();


                //User userss = new User
                //{
                //    Email = "yoourmail@gmail.com",
                //    Name = "Christian Schou",
                //    CreditCard = new CreditCard
                //    {
                //        Name = "Christian Schou",
                //        CardNumber = "4242424242424242",
                //        ExpirationYear = "2024",
                //        ExpirationMonth = "12",
                //        CVC = "999"
                //    }
                //};




           

                modelServices.Note = Request.Form["note"];


                string mypath = @"/Images/ProductModelService/";
                int length = files.Count;

                foreach (var file in files)
                {
                    var basePath = Path.Combine(_environment.WebRootPath + "\\Images\\ProductModelService\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);


                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var filePath = Path.Combine(basePath, file.FileName);
                    var extension = Path.GetExtension(file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }



                    modelServices.ProductModelImages += _configuration.GetSection("APIURL").Value.ToString() + mypath + fileName + extension + ",";






                }
                modelServices.CreatedBy = user.UserId;
                modelServices.ProductModelImages = modelServices.ProductModelImages.Remove(modelServices.ProductModelImages.LastIndexOf(','), 1);
                _services.AddProductModelPics(modelServices);
                response = new Response
                {
                    ResponseMsg = "Request Successful!",
                    Status = 200,

                    Token = null
                };


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


        [HttpPost("GetAllProductModelServices")]
        public Response GetAllProductModelServices()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _services.GetAllProductModelServices();
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


        [HttpPost("GetAllProductModelServicesbycustomer")]
        public Response GetAllProductModelServicesbycustomer()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                int id = user.UserId;


                var res = _services.GetAllProductModelServicesbycustomer(id);
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


        [HttpGet("DownloadPics")]
        public IActionResult DownloadPics(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {


                user = TokenManager.GetValidateToken(Request);
                //if (user == null) return BadRequest("Un Authorized");

                var webRoot = _environment.WebRootPath;
                var filename = "MyZip.zip";
                var tempOutput = webRoot + "/Images/ProductModelService/" + filename;


                var res = _services.GetAllProductServiceModelPics(id);

                List<string> path = new List<string>();
              
                for (int i = 0; i < res.Count; i++)
                {
                    path.Add( res[i].Replace("https://localhost:44326", _environment.WebRootPath)) ;

                }

                //var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(path);

                response = CustomStatusResponse.GetResponse(200);

                using (ZipOutputStream outputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
                {
                    outputStream.SetLevel(9);
                    byte[] buffer=new byte[4096];

                    for (int i = 0; i < path.Count; i++)
                    {
                        ZipEntry entry=new ZipEntry(path[i]);
                        entry.DateTime = DateTime.Now;
                        entry.IsUnicodeText = true;
                        outputStream.PutNextEntry(entry);


                        using (FileStream file=System.IO.File.OpenRead(path[i]))
                        {
                            int sourceByte;
                            do
                            {
                                sourceByte = file.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, sourceByte);

                            }
                            while (sourceByte > 0);


                        }

                    }

                    outputStream.Finish();
                    outputStream.Flush();
                    outputStream.Close();



                }


                byte[] finalresult = System.IO.File.ReadAllBytes(tempOutput);
                if (System.IO.File.Exists(tempOutput))
                {
                    System.IO.File.Delete(tempOutput);


                }

                if (finalresult==null)
                {
                    throw new Exception("Noting Found");


                }

                return File(finalresult, "application/zip");



              //  return File(archiveData, fileType, archiveName);

            }

            catch (DbException ex)
            {
                response = CustomStatusResponse.GetResponse(600);
                response.ResponseMsg = ex.Message;
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                response = CustomStatusResponse.GetResponse(500);
                // response.Token = TokenManager.GenerateToken(claimDTO);
                response.ResponseMsg = ex.Message;
                return BadRequest(ex.Message);
            }

        }


      
        [HttpPost("UploadModelForOrder")]
        public async Task<Response> UploadModelForOrder(IList<IFormFile> files)
        {
            UserManagement user = null;
            Response response = null;
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                ProductModelServices modelServices = new ProductModelServices();

                modelServices.ProductModelServiceID = Convert.ToInt32(Request.Form["ProductModelServiceID"]);


                string mypath = @"/ProductModels/ProductModelService/";
                int length = files.Count;

                foreach (var file in files)
                {
                    var basePath = Path.Combine(_environment.WebRootPath + "\\ProductModels\\ProductModelService\\");
                    bool basePathExists = System.IO.Directory.Exists(basePath);
                    if (!basePathExists) Directory.CreateDirectory(basePath);


                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var filePath = Path.Combine(basePath, file.FileName);
                    var extension = Path.GetExtension(file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }



                    modelServices.ModelPath += _configuration.GetSection("APIURL").Value.ToString() + mypath + fileName + extension;






                }
                modelServices.CreatedBy = user.UserId;
               _services.UploadServiceModel(modelServices);
                response = new Response
                {
                    ResponseMsg = "Request Successful!",
                    Status = 200,

                    Token = null
                };


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

        [HttpPost("GetAllProductModelServices/{id}")]
        public Response GetAllProductModelServices(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _services.ProductModelServicesDelete(id);
                response = CustomStatusResponse.GetResponse(200);
             
                    response.Data = res;
             
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
