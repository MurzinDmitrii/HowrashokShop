using HowrashokShop.Classes;
using HowrashokShop.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HowrashokShopContext>(
    options => options.UseSqlServer(File.ReadAllText("connectionString.txt")));
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = AuthOptions.ISSUER,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = AuthOptions.AUDIENCE,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
});

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseStatusCodePages();
app.UseMvcWithDefaultRoute();

app.UseAuthentication();
app.UseAuthentication();

app.MapPost("/login", (LoginClass loginData) =>
{
    HowrashokShopContext context = new();
    // ������� ������������ 
    List<Client> personList = context.Clients.ToList();
    Client? person = null;
    foreach (var client in personList)
    {
        if (client.Email == loginData.Email)
        {
            ClientsPassword clientPassword = context.ClientsPasswords.FirstOrDefault(c => c.ClientId == client.Id);
            if (Cryptography.Cryptography.VerifyHashedPassword(clientPassword.Password, loginData.Password))
            {
                person = client;
                break;
            }
        }
    }
    // ���� ������������ �� ������, ���������� ��������� ��� 401
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
    // ������� JWT-�����
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    // ��������� �����
    var response = new
    {
        access_token = encodedJwt,
        username = person.Email
    };

    return Results.Json(response);
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Profile",
        pattern: "Profile",
        defaults: new { controller = "Profile", action = "Index" }
    ).RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "Bearer" });
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Index}/{action=Index}");

app.Run();
