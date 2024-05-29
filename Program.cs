using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using Middleware;
using Token.Services;
using Tasks.Utilities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
});

builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("Admin", policy => policy.RequireClaim("UserType", "Admin"));
    cfg.AddPolicy("User", policy => policy.RequireClaim("UserType", "User"));
});
// ConfigureServices method for service registrations

builder.Services.AddHttpContextAccessor();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

builder.Services.AddTask();
builder.Services.AddUser();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


var app = builder.Build();



app.Map("/favicon.ico", (a) =>
a.Run(async c => await Task.CompletedTask));



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
// app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<MyLogMiddleware>();

app.Run();

