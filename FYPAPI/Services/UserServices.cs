using API.DBManager;
using API.IServices;
using ClassLibrary;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace API.Services
{
    public class UserServices : IUserServices
    {
        private readonly IDapper _dapper;
        public UserServices(IDapper dapper)
        {
            _dapper = dapper;
        }

        public List<City> GetAllCities(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

            return _dapper.GetAll<City>(@"[dbo].[usp_CitiesByCountryId]", parameters);
        }

        public List<Country> GetAllCountries()
        {
            DynamicParameters parameters = new DynamicParameters();

            return _dapper.GetAll<Country>(@"[dbo].[usp_GetAllCountries]", parameters);
        }

        public List<UserManagement> getAllcustomers()
        {
            DynamicParameters parameters = new DynamicParameters();
            return _dapper.GetAll<UserManagement>(@"[dbo].[usp_GetAllCustomers]", parameters);


        }

        public UserManagement GetcustomerId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Get<UserManagement>(@"[dbo].[usp_GetVendorsbyId]", parameters);
        }

        public int Useregistration(UserManagement obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserName", obj.UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserEmail", obj.UserEmail, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserPassword", Secure.EncryptData(obj.UserPassword), DbType.String, ParameterDirection.Input);
            parameters.Add("@MobileNumber", obj.MobileNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserAddress", obj.UserAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("@CityID", obj.CityId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CountryId", obj.CountryId, DbType.Int32, ParameterDirection.Input);

            return _dapper.Insert<int>(@"[dbo].[usp_UserRegistration]", parameters);

        }


    }
}
