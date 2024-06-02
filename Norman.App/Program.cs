using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Norman.App;
using Norman.App.Authorization.Seeds;
using Norman.App.Components;
using Norman.App.Models;
using Norman.Common.Services.Models;
using NourApp.Authorization.Seeds;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
IConfigurationSection azureFunction;

azureFunction = builder.Configuration.GetSection(nameof(AzureFunction));
AzureFunction.FileProvider = azureFunction["FileProvider"]?.ToString();
AzureFunction.EmailProvider = azureFunction["EmailProvider"]?.ToString();
AzureFunction.SubscriptionProvider = azureFunction["SubscriptionProvider"]?.ToString();
AzureFunction.CourseProvider = azureFunction["CourseProvider"]?.ToString();
AzureFunction.AccountProvider = azureFunction["AccountProvider"]?.ToString();
AzureFunction.VerificationProvider = azureFunction["VerificationProvider"]?.ToString();

builder.Services.AddDbContext<ApplicationDbContext>(option => option
           .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpClient();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/signin";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });
builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#region Add Users And Roles
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerProvider>();
var logger = loggerFactory.CreateLogger("app");
try
{
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedAsync(roleManager);
    await DefaultUsers.SeedAdminUserAsync(userManager);
    await DefaultUsers.SeedBasicUserAsync(userManager);
    logger.LogInformation("Data Seeded");
    logger.LogInformation("Application Started");
}
catch (Exception ex)
{
    logger.LogWarning(ex, "An Error");
}
#endregion
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();
//.AddAdditionalAssemblies(typeof(MyButton).Assembly);

app.Run();
