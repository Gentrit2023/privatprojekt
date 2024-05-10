using System.Threading.Tasks;
using FederataFutbollit.Contracts;
using FederataFutbollit.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FederataFutbollit.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccount _userAccount;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserAccount userAccount, ILogger<AccountController> logger)
        {
            _userAccount = userAccount;
            _logger = logger;
        }

[HttpPost("register")]
public async Task<IActionResult> Register(UserDTO userDTO)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    try
    {
        var response = await _userAccount.CreateAccount(userDTO);
        if (!response.Flag)
        {
            return BadRequest(response.Message);
        }
        return Ok(response);
    }
    catch (Exception ex)
    {
        // Log the exception details
        _logger.LogError(ex, "Error registering user");
        return StatusCode(500, "An error occurred while processing your request.");
    }
}



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Login data is missing.");
            }

            var response = await _userAccount.LoginAccount(loginDTO);
            if (!response.Flag)
            {
                return Unauthorized(response.Message);
            }

            return Ok(response);
        }
    }
}
