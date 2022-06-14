using ClassLibrary1;
using System.Collections.Generic;

namespace FYPAPI.IServices
{
    public interface ICategoryServices
    {
        int  AddUpdatecategory(Category obj);
         List<Category> GetAllCategory();

         Category GetCategoryById(int Id);

        int  InActive(int Id);

    }
}
