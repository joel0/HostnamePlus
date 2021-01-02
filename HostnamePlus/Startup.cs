using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;

namespace HostnamePlus
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                Program.getOtherIpJsPath = "/js/GetOtherIp.js";
                Program.mainCssPath = "/css/main.css";
            } else {
                app.UseExceptionHandler("/Error");
                Program.getOtherIpJsPath = "/js/GetOtherIp.min.js";
                Program.mainCssPath = "/css/main.min.css";
            }

            // Note: this is for being reverse proxied. Remove if using Kesteral without a reverse proxy.
            // The reverse proxy server must set the XForwardedFor header.
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Index}/{action=Index}/");
            });
        }
    }
}
