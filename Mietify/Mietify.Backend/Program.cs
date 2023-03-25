using Microsoft.EntityFrameworkCore;

namespace Mietify.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           builder.Services.AddDbContext<MietifyDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("MietifyDB")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection(); 
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}