using API.DBManager;
using API.IServices;
using ClassLibrary;
using ClassLibrary1;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
         //   parameters.Add("@profilepic", obj.PorfileImgPath, DbType.String, ParameterDirection.Input);
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
        public int ApproveVendor(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Insert<int>(@"[dbo].[usp_ApproveVendor]", parameters);
        }

        public int DeleteVendor(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Insert<int>(@"[dbo].[usp_DeleteVendor]", parameters);
        }
        public int ActiveInActiveVendor(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Insert<int>(@"[dbo].[usp_ActiveInActiveVendor]", parameters);
        }



        public DashBoardModel GetVendordashboardData(int id)
        {
            DashBoardModel dashBoardModel = new DashBoardModel();
           

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

            var data = _dapper.GetMultipleObjects(@"[dbo].[Usp_VendorDashBordStatus]", parameters, gr => gr.Read<int>(), gr => gr.Read<int>(), gr => gr.Read<int>(), gr => gr.Read<int>(),  gr => gr.Read<DBProduct>(), gr => gr.Read<CompletedOrders>(), gr => gr.Read<PendingOrders>(), gr => gr.Read<CancelledOrders>(), gr => gr.Read<DBOrderStatus>());


            dashBoardModel.TotalEarned = data.Item1.FirstOrDefault();
           
            dashBoardModel.InventoryStokes = data.Item2.FirstOrDefault();
            dashBoardModel.TotalOrdersCompleted = data.Item3.FirstOrDefault();
            dashBoardModel.Pendingorders = data.Item4.FirstOrDefault();
            dashBoardModel.products = data.Item5.ToList();
            dashBoardModel.completedOrders = data.Item6.FirstOrDefault();
            dashBoardModel.DpendingOrders = data.Item7.Item1.FirstOrDefault();
            dashBoardModel.cancelledOrders = data.Item7.Item2.FirstOrDefault();
            dashBoardModel.orderStatus = data.Item7.Item3.ToList();




            return dashBoardModel;




        }
    }
}
