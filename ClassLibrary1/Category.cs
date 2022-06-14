using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Category
    {
        public int CategoryId { get; set; }


        public string CategoryName { get; set; }


        public string CategoryImage { get; set; }

        public string Description { get; set; }


        public DateTime CreatedOn { get; set; }



        public int CreatedBy { get; set; }


        public DateTime ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public bool IsActive { get; set; }




    }
}
