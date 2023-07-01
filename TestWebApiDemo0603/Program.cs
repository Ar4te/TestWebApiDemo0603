using Extensions.AutofacExt;
using Extensions.DataBase.SqlSugarSetUp;
using Extensions.Filters;
using Extensions.Helper;
using Extensions.JWT;
using Extensions.Mapster;
using Extensions.Serilog;
using Extensions.SnowFlake;
using Extensions.Swagger;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Models.MapsterConfig;
using System.Reflection;

namespace TestWebApiDemo0603
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton(new AppSettings(builder.Configuration));
            builder.Host.AddSerilogSetUp().AddAutofacSetup();
            // Add services to the container.
            builder.Services.AddMapsterSetup(MapsterConfig.SetMapsterConfig(new TypeAdapterConfig()));
            builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ValidFilter>();
            });
            builder.Services.AddSingleton(new YittHelper());
            builder.Services.AddJwtSetup();
            builder.Services.AddMemoryCache();
            builder.Services.AddSqlSugarSetup();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            string xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
            builder.Services.AddSwaggerSetUp(xmlFilePath);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUISetup();
            //}

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}