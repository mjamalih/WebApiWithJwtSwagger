using Microsoft.EntityFrameworkCore;
using WebApiWithJwtSwagger.Models;
using WebApiWithJwtSwagger.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WebApiWithJwtSwagger.Data;
using WebApiWithJwtSwagger.Areas.Identity.Data;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApiWithJwtSwagger.Interfaces;
using WebApiWithJwtSwagger.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WebApiWithJwtSwaggerContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApiWithJwtSwaggerContextConnection' not found.");

builder.Services.AddDbContext<WebApiWithJwtSwaggerContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPaymentRequestService, PaymentRequestService>();

builder.Services.AddDefaultIdentity
    <WebApiWithJwtSwaggerUser>(options => options.SignIn.RequireConfirmedAccount = true).
    AddEntityFrameworkStores<WebApiWithJwtSwaggerContext>();

// Add services to the container.

//builder.Services.AddControllers();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

 //   AddNewtonsoftJson(options =>
 //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     c =>
     {
         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             In = ParameterLocation.Header,
             Description = "Please insert JWT with Bearer into field",
             Name = "Authorization",
             Type = SecuritySchemeType.ApiKey
         });


         c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
     }

    );

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(option =>
option.UseSqlServer(builder.Configuration["ConnectionStrings:DB"]));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    var key = Encoding.UTF8.GetBytes(builder.Configuration["ApplicationSettings:JWT_Secret"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var m = builder.Configuration["ApplicationSettings:JWT_Secret"];

//builder.Services.AddAuthentication(cfg =>
//{
//    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = false;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8
//            .GetBytes(builder.Configuration["ApplicationSettings:JWT_Secret"])
//        ),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };
//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//        {
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuer = true,
//                ValidateAudience = true,
//                ValidateLifetime = true,
//                ValidateIssuerSigningKey = true,
//                ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                ValidAudience = builder.Configuration["Jwt:Audience"],
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//            };
//        });





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
  
}


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapUserInfoEndpoints();

app.Run();
