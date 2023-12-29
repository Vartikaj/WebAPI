
using MySqlConnector;
using System.Data.Common;
using WebApplicationWithDatabaseConnection.Context;
using WebApplicationWithDatabaseConnection.Interface;
using WebApplicationWithDatabaseConnection.Services;

namespace WebApplicationWithDatabaseConnection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<IProduct, SProduct>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            
            // builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
            //builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString["Default"]);
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
