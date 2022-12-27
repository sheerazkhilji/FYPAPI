using ClassLibrary1;
using System.Collections.Generic;

namespace FYPAPI.IServices
{
    public interface IorderServices
    {
        public List<OrderSlip> PlaceOrder(Order obj);

        public List<OrderUserId> GetOrdersByUser(int id);


        public List<OrderUserId> GetOrdersItemsByProductsIds(int id,int orderids);

        public int ChangeOrderStatus(OrderUserId obj);

        public List<OrderSlip> OrderSlip();

        public Ordertracking Ordertracking(string ordernumber);
        public List<PurchedModelColor> GetPurchedModelByOrderAndProductId(Parameter obj);

    }
}
