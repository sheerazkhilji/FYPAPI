using ClassLibrary;
using System.Collections.Generic;

namespace API.IServices
{
    public interface IUserServices
    {

        int Useregistration(UserManagement obj);

        List<Country> GetAllCountries();

        List<City> GetAllCities(int id);


        List<UserManagement> getAllcustomers();

        UserManagement GetcustomerId(int id);


    }
}
