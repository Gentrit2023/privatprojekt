using FederataFutbollit.DTOs;
using static FederataFutbollit.DTOs.ServiceResponses;
namespace FederataFutbollit.Contracts
{
    public interface IUserAccount
    {
         Task<ServiceResponses.GeneralResponse> CreateAccount(UserDTO userDTO);
        Task<ServiceResponses.LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}