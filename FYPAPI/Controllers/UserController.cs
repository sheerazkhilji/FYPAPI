using API.IServices;
using API.Utilites;
using ClassLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Common;
using System.Net.Mail;
using System.Text;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _service;
        public UserController(IUserServices services)
        {
            _service = services;
        }

        [HttpPost("Useregistration")]
        public Response Useregistration(UserManagement obj)
        {

            Response response = new Response();
            try
            {


                var res =  _service.Useregistration(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res > 0)
                {

                    CodeVerification cn = new CodeVerification();
                    cn.Email = obj.UserEmail;

                    var ress = _service.ForgotPassword(cn);
                    response.Data = res;
                    response.ResponseMsg = "Please verify your account from your Email";

                    string to = obj.UserEmail;//To address
                    string from = "muhammadtalhashaikh24@gmail.com"; //From address
                    MailMessage message = new MailMessage(cn.Email, to);
                    string mailbody = "Your Activation code  is "+ress.VerifyCode;
                    message.Subject = "Activation code";
                    message.Body = mailbody;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential("muhammadtalhashaikh24@gmail.com", "xumesdtylobsszks");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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



     
        [HttpPost("GetAllCountries")]
        public Response GetAllCountries()
        {

            Response response = new Response();
            try
            {


                var res = _service.GetAllCountries();
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


        [HttpPost("GetAllCities/{id}")]
        public Response GetAllCities(int id)
        {

            Response response = new Response();
            try
            {


                var res = _service.GetAllCities(id);
                response = CustomStatusResponse.GetResponse(200);
                if (res != null )
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



        [HttpPost("GetcustomerId")]
        public Response GetcustomerId(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.GetcustomerId(id);
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


        [HttpPost("getAllcustomers")]
        public Response getAllcustomers()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);


                var res = _service.getAllcustomers();
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


        [HttpPost("UpdateProfile")]
        public Response UpdateProfile(Vendor obj)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                var id = user.UserId;
                obj.UserId = id;

                var res = _service.UpdateProfile(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res>0)
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
        
        [HttpPost("GetProfileDate")]
        public Response GetProfileDate()
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

                var id = user.UserId;

                var res = _service.GetProfileDate(id);
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

        [HttpPost("ActiveInActivecustomer/{id}")]
        public Response ActiveInActivecustomer(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

               
                var res = _service.ActiveInActivecustomer(id);
                response = CustomStatusResponse.GetResponse(200);
                if (res >0 )
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

        [HttpPost("Deletecustomer/{id}")]
        public Response Deletecustomer(int id)
        {
            UserManagement user = null;
            Response response = new Response();
            try
            {
                user = TokenManager.GetValidateToken(Request);
                if (user == null) return CustomStatusResponse.GetResponse(401);

              
                var res = _service.Deletecustomer(id);
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

        [HttpPost("ForgotPassword")]
        public Response ForgotPassword(CodeVerification obj)
        {
            Response response = new Response(); try
            {
                //obj.Email = HttpContext.Request.Form["Email"].FirstOrDefault(); 
                var res = _service.ForgotPassword(obj); 
                response = CustomStatusResponse.GetResponse(200); 
                if (res != null)
                    {
                        response.Data = res.Email; 
                        response.ResponseMsg = "Link to rest your password has been sent to the registered email.";


                        string to = res.Email;//To address
                        string from = "muhammadtalhashaikh24@gmail.com"; //From address
                        MailMessage message = new MailMessage(from, to);
                        string mailbody = "Your Verification Code is " + res.VerifyCode;
                        message.Subject = "Reset Password";
                        message.Body = mailbody;
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp
                        System.Net.NetworkCredential basicCredential1 = new
                        System.Net.NetworkCredential("muhammadtalhashaikh24@gmail.com", "xumesdtylobsszks");
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = basicCredential1;
                        try
                        {
                            client.Send(message);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
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

        [HttpPost("ResetPassword")]
        public Response ResetPassword(CodeVerification obj)
        {
            Response response = new Response();
            try
            {
                //obj.VerifyCode = obj.VerifyCode.FirstOrDefault();
                //obj.Email = HttpContext.Request.Form["Email"].FirstOrDefault();
                //obj.Password = HttpContext.Request.Form["Password"].FirstOrDefault();
                var res = _service.ResetPassword(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res != null)
                {
                    #region Set New Entry In Cache
                    #endregion
                    response.Data = res;
                    response.ResponseMsg = "Password changed Successfuly!";
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


        [HttpPost("VerifyAccountByCode")]
        public Response VerifyAccountByCode(CodeVerification obj)
        {
            Response response = new Response();
            try
            {
               var res = _service.VerifyAccountByCode(obj);
                response = CustomStatusResponse.GetResponse(200);
                if (res >0)
                {
                    #region Set New Entry In Cache
                    #endregion
                    response.Data = res;
                    response.ResponseMsg = "Account Has Been Verified Please Login";
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


    }
}
