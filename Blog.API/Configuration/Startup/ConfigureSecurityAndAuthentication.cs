using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureSecurityAndAuthentication(this IServiceCollection services)
        {
            // TODO: Configure as required
            return services;
        }

        public static IApplicationBuilder ConfigureSecurityHeaders(this IApplicationBuilder app,
            IHostingEnvironment env)
        {
            // TODO: Update and configure as required

            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            return app;
        }

        public static IApplicationBuilder ConfigureAuthentication(this IApplicationBuilder app)
        {
            // TODO: Configure as required
            
            return app;
        }
    }
}