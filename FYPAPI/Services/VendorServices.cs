using API.DBManager;
using API.IServices;
using ClassLibrary;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace API.Services
{
    public class VendorServices: IVendorServices
    {
        private readonly IDapper _dapper;
        public VendorServices(IDapper dapper)
        {
            _dapper = dapper;
        }

        public List<Vendor> getAllVendors()
        {
            DynamicParameters parameters = new DynamicParameters();
            return _dapper.GetAll<Vendor>(@"[dbo].[usp_GetAllVendors]", parameters);


        }
        public List<Vendor> getAllVendorsForApprovals()
        {
            DynamicParameters parameters = new DynamicParameters();
            return _dapper.GetAll<Vendor>(@"[dbo].[usp_GetAllVendorsForApprovals]", parameters);


        }
        public Vendor GetVendorById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Get<Vendor>(@"[dbo].[usp_GetVendorsbyId]", parameters);

        }

        public int SelfVendorRegistration(Vendor obj)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserName", obj.UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserEmail", obj.UserEmail, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserPassword",Secure.EncryptData (obj.UserPassword), DbType.String, ParameterDirection.Input);
            parameters.Add("@MobileNumber", obj.MobileNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserAddress", obj.UserAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("@CityID", obj.CityId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CountryId", obj.CountryId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@StoreName", obj.StoreName, DbType.String, ParameterDirection.Input);
            parameters.Add("@StoreAddress", obj.StoreAddress, DbType.String, ParameterDirection.Input);
           return _dapper.Insert<int>(@"[dbo].[usp_SelfVendorRegistration]", parameters);

        }

        public int Approve(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id",id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Insert<int>(@"[dbo].[usp_Approve]", parameters);
        }

        public int DisApprove(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Insert<int>(@"[dbo].[usp_DisApprove]", parameters);
        }

        public int DeleteVendor(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Insert<int>(@"[dbo].[usp_DeleteVendor]", parameters);
        }
    }
}
