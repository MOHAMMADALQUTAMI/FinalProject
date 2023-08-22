using FinalProject.Entity;
using FinalProject.DAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = builder.Configuration;
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DbAccess>(options =>
{
    options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
});


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DbAccess>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});




builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(DataClamis.IsAdmin_PolicyName, policy => policy.RequireClaim(DataClamis.IsAdmin_ClaimName));
    o.AddPolicy(DataClamis.IsShop_PolicyName, policy => policy.RequireClaim(DataClamis.IsShop_PolicyName));
    o.AddPolicy(DataClamis.IsUser_PolicyName, policy => policy.RequireClaim(DataClamis.IsUser_ClaimName));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI();

app.MapFallbackToFile("index.html");

app.Run();
