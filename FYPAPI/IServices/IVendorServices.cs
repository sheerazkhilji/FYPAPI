using ClassLibrary;
using System.Collections.Generic;

namespace API.IServices
{
    public interface IVendorServices
    {


        int SelfVendorRegistration(Vendor obj);

        List<Vendor> getAllVendors();

        Vendor GetVendorById(int id);
       List<Vendor> getAllVendorsForApprovals();

        int Approve(int id);


        int DisApprove(int id);
        int ApproveVendor(int id);


        int DeleteVendor(int id);

         int ActiveInActiveVendor(int id);

    }
}
