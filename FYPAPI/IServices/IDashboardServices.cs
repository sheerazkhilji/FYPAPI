using ClassLibrary1;
using System.Collections.Generic;

namespace FYPAPI.IServices
{
    public interface IDashboardServices
    {
         DashBoardModel GetdashboardDate();
        public List<OrderUserId> GetOrdersList();
        List<OrderUserId> GetOrdersItemsByProductsIds(RequestParameters obj);

    }
}
