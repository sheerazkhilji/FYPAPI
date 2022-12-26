using API.DBManager;
using ClassLibrary;
using ClassLibrary1;
using Dapper;
using FYPAPI.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FYPAPI.Services
{
    public class ProductServices: IProductServices
    {
        private readonly IDapper _dapper;
        public ProductServices(IDapper dapper)
        {
            _dapper = dapper;
        }

        public int AddUpdateproduct(Product obj)
        {
            var table1 = new DataTable();
            table1.Columns.Add("Type", typeof(string));
            table1.Columns.Add("LayerName", typeof(string));
            table1.Columns.Add("Color", typeof(string));

            foreach (var item in obj.productModelColors)
            {
                var row = table1.NewRow();
                row["Type"] = Convert.ToString(item.Type);
                row["LayerName"] = Convert.ToString(item.LayerName);
                row["Color"] = Convert.ToString(item.Color);
               

                table1.Rows.Add(row);
            }




            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", obj.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CategoryId", obj.CategoryId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ProductName", obj.ProductName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Description", obj.ProductDes, DbType.String, ParameterDirection.Input);
            parameters.Add("@Price", obj.Price, DbType.Double, ParameterDirection.Input);
            parameters.Add("@Quanity", obj.Quantity, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ProductImage", obj.ProductImage, DbType.String, ParameterDirection.Input);
            parameters.Add("@ProductModelPath", obj.ProductModelpath, DbType.String, ParameterDirection.Input);

            parameters.Add("@UserId", obj.CreatedBy, DbType.Int32, ParameterDirection.Input);


            parameters.Add("@type_ProductModelTypes", table1.AsTableValuedParameter("dbo.type_ProductModelTypes"));

            return _dapper.Insert<int>(@"[dbo].[usp_AddUpdateProduct]", parameters);

        }

        public List<Product> GetAllProduct(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userId", id, DbType.Int32, ParameterDirection.Input);

            return _dapper.GetAll<Product>(@"[dbo].[usp_GetAllProduct]", parameters);
        }

        public List<Product> GetExclusiveProducts()
        {
            DynamicParameters parameters = new DynamicParameters();
          
            return _dapper.GetAll<Product>(@"[dbo].[usp_GetExclusiveProducts]", parameters);
        }
        public List<Categories> GetCategories()
        {
            DynamicParameters parameters = new DynamicParameters();
          
            return _dapper.GetAll<Categories>(@"[dbo].[usp_GetCategories]", parameters);
        }

        public ProductAndModelColor GetProductById(int Id)
        {
            ProductAndModelColor product = new ProductAndModelColor();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);

            var data = _dapper.GetMultipleObjects(@"[dbo].[usp_GetProductById]", parameters, gr => gr.Read<Product>(), gr => gr.Read<ProductModelColor>());

            product.product = data.Item1.FirstOrDefault();

            product.productModelColors = data.Item2.ToList();

            return product;
        }
     

        public Shop GetShopDate()
        {
            DynamicParameters parameters = new DynamicParameters();

            Shop shop = new Shop();

            var data = _dapper.GetMultipleObjects(@"[dbo].[usp_GetDataForShop]", parameters, gr => gr.Read<Category>(), gr => gr.Read<Vendor>(), gr => gr.Read<Product>());
            shop.category = data.Item1.ToList();
            shop.Stores = data.Item2.ToList();
            shop.product = data.Item3.ToList();



            return shop;

        }

        public object GetColorsAndLayernameByID(int id)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@productId", id, DbType.Int32, ParameterDirection.Input);

            var data = _dapper.GetMultipleObjects(@"[dbo].[GetColorsAndLayernameByID]", parameters, gr => gr.Read<ProductPOCO>(), gr => gr.Read<ProductModelColorPOCO>(), gr => gr.Read<UserManagement>());

            SpecialProductObject obj = new SpecialProductObject();

            obj.ProductPOCO = data.Item1.ToList();
            obj.ProductModelColorPOCO = data.Item2.ToList();
          
            obj.VendorInfo=data.Item3.FirstOrDefault(); 
            return obj;

        }

        public int AddReview(ProductsReviews obj)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", obj.UserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ProductId", obj.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@NumberOfStars", obj.NumberOfStars, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Comments", obj.Comments, DbType.String, ParameterDirection.Input);

            return _dapper.Insert<int>(@"[dbo].[usp_AddProductReview]", parameters);
        }




        public int Active_IsActive_Product(int id)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ProductId", id, DbType.Int32, ParameterDirection.Input);

            return _dapper.Update<int>(@"[dbo].[usp_Active_IsActive_Product]", parameters);

        }
    }
}
