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

        public Product GetProductById(int Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", Id, DbType.Int32, ParameterDirection.Input);
            return _dapper.Get<Product>(@"[dbo].[usp_GetProductById]", parameters);

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
    }
}
