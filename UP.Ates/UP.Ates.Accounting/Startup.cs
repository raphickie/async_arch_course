using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using UP.Ates.Accounting.Consumers.TaskTracker.PopugTask;
using UP.Ates.Accounting.Consumers.TaskTracker.TaskAssignedEvent.v1;
using UP.Ates.TaskTracker.Consumers.Auth;
using UP.Ates.TaskTracker.Producers;
using UP.Ates.TaskTracker.Repositories;
using UP.Ates.TaskTracker.Services;

namespace UP.Ates.TaskTracker
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<TaskService>();
            services.AddSingleton<TasksRepository>();
            services.AddSingleton<UserRepository>();
            services.AddHostedService<UserConsumer>();
            services.AddHostedService<TaskAssignedEventConsumer>();
            services.AddHostedService<PopugTaskV1Consumer>();
            services.AddSingleton<PopugProducer>();
            services.AddSingleton(new RepositoryConnectionSettings
            {
                ConnectionString =
                    "Data Source=Accounting.db"
            });
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            IdentityModelEventSource.ShowPII = true;
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5001";

                options.ClientId = "accounting";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                
                options.Scope.Add("api1");

                options.SaveTokens = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                // .RequireAuthorization();
            });
        }
    }
}
