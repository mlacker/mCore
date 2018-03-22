namespace mCore.Services.Identity.API
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using IdentityServer4.Services;
    using mCore.Services.Identity.API.Configuration;
    using mCore.Services.Identity.API.Data;
    using mCore.Services.Identity.API.Models;
    using mCore.Services.Identity.API.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();

            services.AddMvc();

            //callbacks urls from config:
            var clientUrls = new Dictionary<string, string>
            {
                {"Mvc", Configuration.GetValue<string>("MvcClient")},
                {"Spa", Configuration.GetValue<string>("SpaClient")},
                {"Xamarin", Configuration.GetValue<string>("XamarinCallback")},
                {"LocationsApi", Configuration.GetValue<string>("LocationApiClient")},
                {"MarketingApi", Configuration.GetValue<string>("MarketingApiClient")},
                {"BasketApi", Configuration.GetValue<string>("BasketApiClient")},
                {"OrderingApi", Configuration.GetValue<string>("OrderingApiClient")}
            };
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(clientUrls))
                .AddAspNetIdentity<ApplicationUser>()
                .Services.AddTransient<IProfileService, ProfileService>();

            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Identity API",
                    Version = "v1"
                });
            });

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
            });

            // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
            app.Use(async (context, next) =>
            {
                //context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                await next();
            });

            app.UseMvc();
        }
    }
}
