using AuthenticationForm.Core.Models;

namespace AuthentificationForm.BusinessLogic.Authentificator.AuthentificationResults
{
    public class AuthentificationResult
    {
        public AuthentificationResult(User? user)
        {
            User = user;
        }

        public AuthentificationResult(AuthentificationErrors? error)
        {
            Error = error;
        }

        public bool Successfully => Error == null;
        public User? User { get; set; }
        public AuthentificationErrors? Error { get; set; }

        public static AuthentificationResult Success(User user) => new AuthentificationResult(user);
        public static AuthentificationResult Failure(AuthentificationErrors error) => new AuthentificationResult(error);
    }
}