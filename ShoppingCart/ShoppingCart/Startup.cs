using Common.DatabaseSettings;
using Data;
using Data.Abstraction;
using Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoppingCart.Filters;
using FluentValidation;
using Validator;

namespace ShoppingCart
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
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("defaultPoly", corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyMethod();
                    corsPolicyBuilder.AllowAnyOrigin();
                    corsPolicyBuilder.AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingCart", Version = "v1" });
            });

            // database container registration
            services.AddScoped<DatabaseContext>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<GlobalExceptionFilter>();

            services.AddAutoMapper(typeof(ProductMapperProfile).Assembly);
            services.AddValidatorsFromAssembly(typeof(CartProductValidator).Assembly);
            services.AddControllers(opt => {
                opt.Filters.AddService<GlobalExceptionFilter>();
            });


            // configuration (options)
            services.Configure<DbSettings>(Configuration.GetSection(nameof(DbSettings)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("defaultPoly");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}