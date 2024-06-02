using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Norman.Common.Services.Features;
using Norman.Common.Services.Models;
using System.Text;

namespace Norman.AccountProvider.Functions
{
    public class Verify
    {
        private readonly ILogger<Verify> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public Verify(ILogger<Verify> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Function("Verify")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            string body = null!;
            try
            {
                body = await new StreamReader(req.Body).ReadToEndAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"StreamReader :: {ex.Message}");
            }

            if (body != null)
            {
                VerificationRequest vr = null!;
                try
                {
                    vr = JsonConvert.DeserializeObject<VerificationRequest>(body)!;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"JsonConvert.DeserializeObject<VerificationRequest> :: {ex.Message}");
                }
                if (vr != null && !string.IsNullOrEmpty(vr.Email) && !string.IsNullOrEmpty(vr.VerificationCode))
                {
                    // om du vill kora det som en HTTP REQUEST - kraver att vi vantar pa svar tillbakfl
                    try
                    {
                        using var http = new HttpClient();
                        StringContent content = new StringContent(JsonConvert.SerializeObject(vr), Encoding.UTF8, "application/json");
                        // var response = await http.PostAsync("https://verificationprovider.siliconas.azurewebsite.net/api/verify", content);

                        if (true)
                        {
                            var userAccount = await _userManager.FindByEmailAsync(vr.Email);
                            if (userAccount != null)
                            {
                                userAccount.EmailConfirmed = true;
                                await _userManager.UpdateAsync(userAccount);

                                if (await _userManager.IsEmailConfirmedAsync(userAccount))
                                {
                                    return new OkResult();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"JsonConvert.DeserializeObject<UserRegistrationRequest> :: {ex.Message}");
                    }

                }
            }
            return new UnauthorizedResult();
        }
    }
}
