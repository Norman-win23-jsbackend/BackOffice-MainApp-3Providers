using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Norman.Common.Services.Features;
using Norman.Common.Services.Models;

namespace Norman.AccountProvider.Functions
{
    public class SignIn(ILogger<SignIn> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManger)
    {
        private readonly ILogger<SignIn> _logger = logger;
        private readonly UserManager<ApplicationUser> _userManger = userManger;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        [Function("SignIn")]
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
                SignInRequest request = null!;
                try
                {
                    request = JsonConvert.DeserializeObject<SignInRequest>(body)!;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"JsonConvert.DeserializeObject<UserLoginRequest> :: {ex.Message}");
                }

                if (request != null && !string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password))
                {
                    try
                    {
                        var user = await _userManger.Users.FirstOrDefaultAsync(e => e.UserName == request.Username);
                        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
                        if (result.Succeeded)
                        {
                            var role = await _userManger.GetRolesAsync(user);
                            return new OkObjectResult(new SignInResponse() { Username = request.Username, Role = role[0] });
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"_signInManager.PasswordSignInAsync :: {ex.Message}");
                    }
                    return new UnauthorizedResult();
                }
                else
                {
                    return new ConflictResult();
                }
            }
            return new BadRequestResult();
        }
    }
}
