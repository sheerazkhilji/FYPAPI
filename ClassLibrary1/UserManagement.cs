using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class UserManagement: City
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string RoleName { get; set; }

        public string UserEmail { get; set; }
        public string UserPassword { get; set; }


        public  string MobileNumber { get; set; }



        public string UserAddress { get; set; }



        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
                                                 
        public bool IsActive { get; set; }

        public bool IsEmailVerifired { get; set; }
        public bool IsApproved { get; set; }

        public bool IsDelete { get; set; }

        public string Status { get; set; }


        public string PorfileImgPath { get; set; }
        public string FileExtension { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public string label { get; set; }

        public int value { get; set; }
    }
    public class City:Country
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

    }

    public  class Vendor : UserManagement
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }


        public string StoreAddress { get; set; }

        public string StoreImage { get; set; }


    }

    public class LoginCredentials
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }


    }

    public class CodeVerification { 
        public int Id { get; set; } 
        public string Email { get; set; } 
        public string VerifyCode { get; set; } 
        public string Password { get; set; } 
    }



}
