using HowrashokShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HowrashokShopContext>(
    options => options.UseSqlServer(File.ReadAllText("connectionString.txt")));
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseStatusCodePages();
app.UseMvcWithDefaultRoute();

app.Run();
