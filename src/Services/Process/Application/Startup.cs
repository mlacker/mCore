using System;
using System.Reflection;
using Autofac;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using mCore.Services.Process.Application.Definition;
using mCore.Services.Process.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mCore.Services.Process.Application
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
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "process";
                });
            
            services.Add(new ServiceDescriptor(typeof(IMapper), ConfigureMapper, ServiceLifetime.Singleton));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Application Service
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("AppService"))
                .AsSelf()
                .InstancePerLifetimeScope();

            // Core Service
            builder.RegisterAssemblyTypes(typeof(Core.Engine.RuntimeService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf()
                .InstancePerLifetimeScope();

            // Repository
            builder.RegisterAssemblyTypes(typeof(ApplicationDbContext).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Entity Framework
            builder.RegisterType<ApplicationDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

        }

        public IMapper ConfigureMapper(IServiceProvider provider)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AddProfiles(Assembly.GetExecutingAssembly());
            });

            return config.CreateMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
