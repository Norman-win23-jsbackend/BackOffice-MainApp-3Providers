
using Microsoft.AspNetCore.Identity;

namespace Norman.Common.Services.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? ProfilePicture { get; set; } = "avatar.jpg";
        public string? Biography { get; set; }
        public string? AddressLine_1 { get; set; }
        public string? AddressLine_2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
    }
}
