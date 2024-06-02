namespace Norman.Common.Services.Features
{
    public class VerificationRequest
    {
        public string Email { get; set; } = null!;
        public string VerificationCode { get; set; } = null!;
    }

}
