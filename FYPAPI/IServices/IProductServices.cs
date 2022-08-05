using ClassLibrary1;
using System.Collections.Generic;

namespace FYPAPI.IServices
{
    public interface IProductServices
    {
        int AddUpdateproduct(Product obj);
        List<Product> GetAllProduct(int id);
        public ProductAndModelColor GetProductById(int Id);


        List<Product> GetExclusiveProducts();



        Shop GetShopDate();



    }
}
