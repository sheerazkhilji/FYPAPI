using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DashBoardModel
    {
        public int TotalVendors { get; set; }

        public int TotalEarned { get; set; }
        public int InventoryStokes { get; set; }

        public int TotalOrdersCompleted { get; set; }
        public int TotalCustomerCount { get; set; }

        public int Pendingorders { get; set; }
        public int Revenue { get; set; }
        public List<DBProduct> products { get; set; }

        public DBVendor vendor { get; set; }
        public DBCustomers Customers { get; set; }
        
        public List<CustomerVendorTrafic> customerVendorTrafic { get; set; }


        public List<DBOrderStatus> orderStatus { get; set; }


        public CompletedOrders completedOrders { get; set; }


        public PendingOrders DpendingOrders { get; set; }

        public CancelledOrders cancelledOrders { get; set; }
    }



    public class DBVendor
    {

        public int value { get; set; }
        public string name { get; set; }



    }


    public class DBCustomers
    {

        public int value { get; set; }
        public string name { get; set; }



    }


    public class CustomerVendorTrafic
    {

        public DBVendor vendor { get; set; }
        public DBCustomers customers { get; set; }


    }


    public class DBOrderStatus
    {


        public int Total { get; set; }
        public string Orderstatus { get; set; }
    }
    public class DBProduct
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string StoreName { get; set; }
        public string UserName { get; set; }

        public float Revenue { get; set; }

    }

    public class CompletedOrders
    {


        public int value { get; set; }
        public string Orderstatus { get; set; }
    }

    public class PendingOrders
    {


        public int value { get; set; }
        public string Orderstatus { get; set; }
    }
       public class CancelledOrders
    {


        public int value { get; set; }
        public string Orderstatus { get; set; }
    }

 

     public class RequestParameters
    {

        public int OrderId { get; set; }

        public int UserId { get; set; }


        public string StoreName { get; set; }
    }

}
