using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int StoreId { get; set; }
        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }

        public string ProductDes { get; set; }
        public string ProductModelpath { get; set; }

        public string ProductImage { get; set; }


        public DateTime createdOn { get; set; }


        public List<ProductModelColor> productModelColors { get; set; }
        public int ProductRating { get; set; }
        public int Ratingcount { get; set; }

        public float Revenue { get; set; }

    }


    public  class ProductModelColor
    {
        public int ProductModelColorId { get; set; }

        public int ProductId { get; set; }

        public string Type { get; set; }
        public string GroupedColors { get; set; }

        public string LayerName { get; set; }

        public string Color { get; set; }


    }


    public class ProductAndModelColor
    {
        public Product product { get; set; }
        public List<ProductModelColor> productModelColors { get; set; }


    }



    public class ProductPOCO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public DateTime ReviewDate { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int StarPercentage { get; set; }
        public int Indv_StarPercentage { get; set; }
        public int TotalNumberOfStars { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int NumberOfStars { get; set; }
        public int StoreId { get; set; }
        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }

        public string ProductDes { get; set; }
        public string ProductModelpath { get; set; }

        public string ProductImage { get; set; }

        public string Comments { get; set; }

    }

    public class ProductModelColorPOCO
    {
  
        public string GroupedColors { get; set; }

        public string LayerName { get; set; }



    }


    public class SpecialProductObject
    {

        public List<ProductPOCO> ProductPOCO { get; set; }
        public List<ProductModelColorPOCO> ProductModelColorPOCO { get; set; }
    }


    public class ProductsReviews
    {


        public int UserId { get; set; }

        public int ProductId { get; set; }
        public int NumberOfStars { get; set; }

        public string Comments { get; set; }

    }
         


}
