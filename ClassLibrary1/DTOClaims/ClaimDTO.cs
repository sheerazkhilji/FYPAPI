using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DTOClaims
{
   public  class ClaimDTO
    {
        public UserManagement userManagement { get; set; }

        public List<string> RoleName { get; set; }

    }
}
