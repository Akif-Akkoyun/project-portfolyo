using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Mvc;
using ServiceStack;
using ServiceStack.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddHttpClient("auth-api", c =>
{
    c.BaseAddress = new Uri("https://localhost:7117");
});

builder.Services.AddHttpClient("data-api", c =>
{
    c.BaseAddress = new Uri("https://localhost:7215");
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IUserService, UserService>();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
