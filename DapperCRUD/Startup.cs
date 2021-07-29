using DapperCRUD.Aplication.CRUD;
using DapperCRUD.Core.Persistence.DapperConnection;
using DapperCRUD.Infrastructure.Interfaz;
using DapperCRUD.Infrastructure.Repository;
using DapperCRUD.Middleware;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DapperCRUD
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
            services.AddCors(x => x.AddPolicy("corsDapper", builder =>
              {
                  builder.WithOrigins("*").
                  AllowAnyMethod().
                  AllowAnyHeader();

              }));
            services.AddControllers().AddJsonOptions(z => z.JsonSerializerOptions.IgnoreNullValues = true).
                AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<NewInsert>());

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Dapper Crud",
                        Version = "v1"
                    });
                    c.CustomSchemaIds(c => c.FullName);
                }
            );

            services.AddMediatR(typeof(NewInsert.ExecuteRequestAdd).Assembly);

            services.Configure<ConnectionConfiguration>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IStudents, RepositoryStudent>();
            services.AddTransient<IFactoryConnection, FactoryConnection>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HandlerErrorMiddleware>();


            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("corsDapper");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Dapper Web API");
            });

        }
    }
}
