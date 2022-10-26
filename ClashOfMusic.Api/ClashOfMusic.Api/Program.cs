using ClashOfMusic.Api.Configuration;
using ClashOfMusic.Api.Data;
using ClashOfMusic.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ClashOfMusic.Api.Configuration.Abstractions;
using ClashOfMusic.Api.Configuration.Seeding;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default");
//Configuration["connectionStrings:Default"];

builder.Services.AddDbContext<ClashOfMusicContext>(options => {
    options.UseSqlServer(connectionString);
});



 
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve); ;
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<ClashOfMusicContext>();

var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
builder.Services.Configure<JwtBearerTokenSetting>(jwtSection);
var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSetting>();
var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtBearerTokenSettings.Issuer,
        ValidAudience = jwtBearerTokenSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var seedService =  app.Services.GetService<ISeedDataToDB>();
if(seedService != null)
    await seedService.SeedNeccessaryData();

app.Run();
