using Hotel.Server.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Hotel.Server.Repositories;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Interfaces;
using Hotel.Server.Services;

namespace Hotel.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel.Server", Version = "v1" });
            });

            services.AddDbContext<HotelContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel.Server v1"));
            }

            app.UseHttpsRedirection();

            // this works for specific origins
            //app.UseCors(policy =>
            //    policy.WithOrigins(
            //        "https://localhost:44394",
            //        "https://localhost:5001"
            //    )
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //);

            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
