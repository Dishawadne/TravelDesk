
using Microsoft.EntityFrameworkCore;
using TravelDesk.Context;

using TravelDesk.IRepository;
using TravelDesk.Repository;




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

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
           // builder.Services.AddScoped<ITravelRequestRepository, TravelRequestRepository>();
            builder.Services.AddDbContext<DbContexts>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

         builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
         {
           builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
         }));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
