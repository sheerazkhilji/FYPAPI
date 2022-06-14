using API.DBManager;
using ClassLibrary1;
using Dapper;
using FYPAPI.IServices;
using System.Collections.Generic;
using System.Data;

namespace FYPAPI.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IDapper _dapper;
        public CategoryServices(IDapper dapper)
        {
            _dapper = dapper;
        }
        public int AddUpdatecategory(Category obj)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", obj.CategoryId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CategoryName", obj.CategoryName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Description", obj.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("@CategoryImage", obj.CategoryImage, DbType.String, ParameterDirection.Input);
            parameters.Add("@UserId", obj.CreatedBy, DbType.Int32, ParameterDirection.Input);
            
            return _dapper.Insert<int>(@"[dbo].[usp_AddUpdateCategory]", parameters);
        }


        public List<Category> GetAllCategory()
        {

            DynamicParameters parameters = new DynamicParameters();
            
            return _dapper.GetAll<Category>(@"[dbo].[usp_GetAllCategory]", parameters);
        }

        public Category GetCategoryById(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", Id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Get<Category>(@"[dbo].[usp_GetCategoryByID]", parameters); 

        }

        public int InActive(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CategoryId", Id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Get<int>(@"[dbo].[usp_UpdateCategory]", parameters);
        }
    }
}
