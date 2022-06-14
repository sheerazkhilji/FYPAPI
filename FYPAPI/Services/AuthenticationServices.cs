using API.DBManager;
using API.IServices;
using ClassLibrary;
using ClassLibrary.DTOClaims;
using Dapper;
using System.Data;
using System.Linq;

namespace API.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IDapper _dapper;
        public AuthenticationServices(IDapper dapper)
        {
            _dapper = dapper;

        }

        public ClaimDTO Authenticate(LoginCredentials obj)
        {

        
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserEmail", obj.UserEmail, DbType.String, ParameterDirection.Input);
                parameters.Add("@UserPassword", Secure.EncryptData(obj.Password), DbType.String, ParameterDirection.Input);
                var tuple = _dapper.GetMultipleObjects(@"[dbo].[usp_ValidateLogin]", parameters, gr => gr.Read<UserManagement>(), gr => gr.Read<string>());

                ClaimDTO claimDTO = new ClaimDTO();
                claimDTO.userManagement = tuple.Item1.FirstOrDefault();
            claimDTO.RoleName = tuple.Item2.ToList();


                if (claimDTO.userManagement != null)
                {
                    return claimDTO;
                }
                else
                {
                    return null;
                }

                   }
    }
}
