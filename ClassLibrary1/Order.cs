//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Order
    {
        public int OrderId { get; set; }

        public string ProductsIds { get; set; }

        public int PaymentMethodType { get; set; }
        public bool IsShippingDifferentAddress { get; set; }

        public string OrderStatus { get; set; }

        public string OrderNumber { get; set; }

        public string Deliverytime { get; set; }

        public string orderdate { get; set; }
        public string AdditionalInfo { get; set; }

        public int UserId { get; set; }


        public string Name { get; set; }

        public string MobileNumber { get; set; }
        public string CityName { get; set; }

        public string Address { get; set; }

    }



    public class OrderUserId
    {
        public int customerId { get; set; }
        public int Vendor { get; set; }
        public string customername { get; set; }
        public int Quantity { get; set; }

        public float TotalPrice { get; set; }
        public float Price { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public int OrderId { get; set; }
        public int StoreId { get; set; }

        public string OrderStatus { get; set; }

        public string CityName { get; set; }

        public string UserAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Orderids { get; set; }

        public string UserEmail { get; set; }

    }



   



    public class OrderSlip
    {
        public string OrderNumber { get; set; }
        public string customer { get; set; }
        public string Address { get; set; }
        public int Quanlity { get; set; }

    public string productcount { get; set; }
    public string AdditionalInfo { get; set; }

        public string PaymentMethodType { get; set; }

        public float Price { get; set; }

        public float TotalPrice { get; set; }
        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public string UserName { get; set; }
        public string UserAddress { get; set; }

        public DateTime OrderDate { get; set; }

        public string MobileNumber { get; set; }

        public string UserEmail { get; set; }
        public float OrderPrice { get; set; }


    }



    public class Ordertracking
    {


        public Order order { get; set; }

        public List<OrderSlip>  CustomerOrderItems { get; set; }

        public List<OrderSlip>  OrderNeedtoBeDelivered { get; set; }
        public List<OrderSlip>  OrderNeedNottoBeDelivered { get; set; }



}


//public class Slip
//    {
//        Document _document;
//        Font _FontStyle;
//        PdfPTable _pdfPTable;
//        PdfPCell _pdfPCell;
//        MemoryStream _memoryStream = new MemoryStream();

//        List<OrderUserId> _orderUsers = new List<OrderUserId>();
 

//        public byte[] PrepareSlip(List<OrderUserId> orderUsers)
//        {

//            _orderUsers = orderUsers;
//            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
//            _document.SetPageSize(PageSize.A4);
//            _document.SetMargins(20f, 20f, 20f, 20f);
//            _pdfPTable.WidthPercentage = 100;
//            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
//            _FontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

//            PdfWriter.GetInstance(_document, _memoryStream);
//            _document.Open();
//            _pdfPTable.SetWidths(new float[] { 20f, 150f, 100f });
//            _pdfPTable.HeaderRows = 2;
//            _document.Add(_pdfPTable);
//            _document.Close();
//            return _memoryStream.ToArray();
//        }

        
//    }

}
