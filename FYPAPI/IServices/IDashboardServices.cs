using ClassLibrary1;
using System.Collections.Generic;

namespace FYPAPI.IServices
{
    public interface IDashboardServices
    {
         DashBoardModel GetdashboardDate();
        public List<OrderUserId> GetOrdersList();
        List<OrderUserId> GetOrdersItemsByProductsIds(RequestParameters obj);
         int AddProductModelPics(ProductModelServices obj);

        public List<ProductModelServices> GetAllProductModelServicesbycustomer(int id);
        public List<ProductModelServices> GetAllProductModelServices(); 
        public List<string> GetAllProductServiceModelPics(int id);
        public int UploadServiceModel(ProductModelServices obj);
        public int ProductModelServicesDelete(int id);


    }
}
