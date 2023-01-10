using Microsoft.AspNetCore.Mvc;

namespace AuthenticationForm.Host.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        public dynamic ReturnStatusCode(int statusCode, object? content) 
        {
            return StatusCode(statusCode, new 
            {
                StatusCode = statusCode,
                Content = content,
            });
        }
    }
}
