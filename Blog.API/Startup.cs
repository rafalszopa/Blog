using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nyami.AspNetCore.VueCliServices;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Blog.API.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using Infrastructure.Security.IoC;
using System;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public string ConnectionString => Configuration.GetConnectionString("BlogDatabase");

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "../Blog.Admin/dist"; });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Blog.Authentication.Cookie";
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.CompletedTask;
                    };
                });

            services.AddSecurityServices(this.ConnectionString);
            services.AddDatabaseMigration(this.ConnectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../Blog.Admin";

                if (env.IsDevelopment())
                {
                    spa.UseVueCliServer(npmScript: "serve");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(200);
                }
            });

            migrationRunner.MigrateUp();
        }
    }
}
