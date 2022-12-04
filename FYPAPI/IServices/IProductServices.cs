using ClassLibrary1;
using System.Collections.Generic;

namespace FYPAPI.IServices
{
    public interface IProductServices
    {
        int AddUpdateproduct(Product obj);
        List<Product> GetAllProduct(int id);
        public ProductAndModelColor GetProductById(int Id);
        public object GetColorsAndLayernameByID(int id);


        List<Product> GetExclusiveProducts();

        List<Categories> GetCategories();
        Shop GetShopDate();


        int AddReview(ProductsReviews obj);




    }
}
