using Microsoft.AspNetCore.Mvc;
using ModelAgency_Api.Models;
using ModelAgency_Api.Services;

namespace ModelAgency_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<User> Login(User user)
        {
            return await _userService.Login(user);
        }
    }
}
