using ClassLibrary;
using ClassLibrary.DTOClaims;

namespace API.IServices
{
    public interface IAuthenticationServices
    {
        ClaimDTO Authenticate(LoginCredentials obj);
    }
}
