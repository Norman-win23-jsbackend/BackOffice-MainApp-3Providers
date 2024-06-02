using System.ComponentModel.DataAnnotations;

namespace Norman.Common.Services.Features
{
    public class SignInRequest
    {
        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "The Password field must be a minimum of 8 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class SignInResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
