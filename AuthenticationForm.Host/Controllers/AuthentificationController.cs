using AuthenticationForm.Core.DTOs;
using AuthenticationForm.Host.Models.Responses;
using AuthentificationForm.BusinessLogic.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthenticationForm.Host.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthentificationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthentificationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [SwaggerResponse(200, "returns info about a user", typeof(UserInfo))]
        [SwaggerResponse(404, "user is not exists")]
        public IActionResult Login(UserDTO dto)
        {
            var result = _userService.Login(dto);

            return StatusCode(
                (int)result.StatusCode, 
                result.User == null ? null : new UserInfo()
                {
                    Email = result.User.Email,
                    DateOfRegistration = result.User.DateOfRegistration,
                });
        }

        [HttpPost]
        [SwaggerResponse(200, "returns info about a new user", typeof(UserInfo))]
        [SwaggerResponse(406, "user is exists")]
        public IActionResult Registration(UserDTO dto)
        {
            var result = _userService.Registration(dto);

            return StatusCode(
                (int)result.StatusCode, 
                result.User == null ? null : new UserInfo()
                {
                    Email = result.User.Email,
                    DateOfRegistration = result.User.DateOfRegistration,
                });
        }

        [HttpGet]
        [Authorize]
        [SwaggerResponse(200, "returns info about a new user", typeof(UserInfo))]
        [SwaggerResponse(404, "user is exists")]
        public IActionResult GetLoginedUser()
        {
            var user = _userService.GetLoginedUser();
            int statusCode = user == null ? 404 : 200;

            return StatusCode(statusCode, user == null ? null : new UserInfo()
            {
                Email = user.Email,
                DateOfRegistration = user.DateOfRegistration,
            });
        }
    }
}
