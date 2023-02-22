using ClashOfMusic.Api.Configuration;
using ClashOfMusic.Api.Configuration.Abstractions;
using ClashOfMusic.Api.Configuration.Seeding;
using ClashOfMusic.Api.Data;
using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Data.Repositories;
using ClashOfMusic.Api.Mapper;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);


//Configuration["connectionStrings:Default"];
builder.Services.AddTransient<ISeedDataToDB, SeedDataToDB>();
builder.Services.AddTransient<IYoutubeSearchServices, YoutubeSearchServices>();
builder.Services.AddTransient<IPlayListServices, PlayListServices>();
builder.Services.AddTransient<IPlayListRepository, PlayListRepositiory>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new MapperProfile());
});

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddCors();
builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<ClashOfMusicContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<ClashOfMusicContext>();

var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSetting");
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

//SeedDatabase();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//using (var scope = app.Services.CreateScope())
//{
//    var dbInitializer = scope.ServiceProvider.GetRequiredService<ISeedDataToDB>();
//    dbInitializer.SeedNeccessaryData();
//}
//await app.Services.GetService<SeedDataToDB>().SeedNeccessaryData();
////if(seedService != null)
//     await seedService.SeedNeccessaryData();

app.Run();
