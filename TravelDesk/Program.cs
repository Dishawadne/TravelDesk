using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TravelDesk.Context;
using TravelDesk.IRepository;
using TravelDesk.Repository;
using TravelDesk.Controllers;
namespace TravelDesk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = builder.Configuration["Jwt:Issuer"],
                      ValidAudience = builder.Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                  };
              });
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<ITravelRequestRepository, TravelRequestRepository>();
            builder.Services.AddTransient<RoleController>();
            builder.Services.AddDbContext<DbContexts>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
         builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
         {
           builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
         }));
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
