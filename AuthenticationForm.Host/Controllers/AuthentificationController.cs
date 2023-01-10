using AuthenticationForm.Core.DTOs;
using AuthenticationForm.Host.Models.Responses;
using AuthentificationForm.BusinessLogic.Services.UserServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

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
        public dynamic Login(UserDTO dto)
        {
            var result = _userService.Login(dto);

            return new
            {
                StatusCode = result.StatusCode,
                Content = result.User == null ? null : new UserInfo()
                {
                    Email = result.User.Email,
                    DateOfRegistration = result.User.DateOfRegistration,
                },
            };
        }

        [HttpPost]
        [SwaggerResponse(200, "returns info about a new user", typeof(UserInfo))]
        [SwaggerResponse(406, "user is exists")]
        public dynamic Registration(UserDTO dto)
        {
            var result = _userService.Registration(dto);

            return new
            {
                StatusCode = result.StatusCode,
                Content = result.User == null ? null : new UserInfo()
                {
                    Email = result.User.Email,
                    DateOfRegistration = result.User.DateOfRegistration,
                },
            };
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public dynamic GetLoginedUser()
        {
            var user = _userService.GetLoginedUser();
            int statusCode = user == null ? 404 : 200;

            return StatusCode(statusCode, new 
            {
                StatusCode = statusCode,
                Content = user,
            });
        }
    }
}
