using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Norman.BackOffice;
using Norman.BackOffice.Components;
using Norman.BackOffice.Models;
using Norman.Common.Services.Models;

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
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
