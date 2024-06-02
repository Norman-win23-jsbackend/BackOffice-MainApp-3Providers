namespace Norman.Common.Services.Features
{
    public class SignUpRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; }
        public bool TermsConditions { get; set; }
    }
}
