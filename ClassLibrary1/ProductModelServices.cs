using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
  public   class ProductModelServices
    {
        public int ProductModelServiceID { get; set; }

        public string Note { get; set; }

        public string status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }


        public bool IsActive { get; set; }


        public string ModelPath { get; set; }


        public string ProductModelImages { get; set; }


        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        
    }
}
