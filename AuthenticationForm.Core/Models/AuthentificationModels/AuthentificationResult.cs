using AuthenticationForm.Core.Models;

namespace AuthentificationForm.Core.Models.AuthentificationModels
{
    public class AuthentificationResult
    {
        public AuthentificationResult(AuthentificationResponses statusCode, User? user = null)
        {
            StatusCode = statusCode;
            User = user;
        }

        public bool Successfully => User != null;
        public AuthentificationResponses StatusCode { get; set; }
        public User? User { get; set; }

        public static AuthentificationResult Success(User user) => new AuthentificationResult(AuthentificationResponses.Success, user);
        public static AuthentificationResult Failure(AuthentificationResponses statusCode) => new AuthentificationResult(statusCode);
    }
}