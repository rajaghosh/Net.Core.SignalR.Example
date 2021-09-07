using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoChatService.Abstractions;
using VideoChatService.Hubs;
using VideoChatService.Options;
using VideoChatService.Services;

namespace VideoChatService.WebApi
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
            services.AddControllersWithViews();

            services.Configure<TwilioSettings>(
                settings =>
                {
                    //settings.AccountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"); //SKbca8712511b92b145de7921da52c3e7d
                    //settings.ApiSecret = Environment.GetEnvironmentVariable("TWILIO_API_SECRET"); //1uBK4bJrP2VNUxJwzdeIhKPVfAdSqwPe
                    //settings.ApiKey = Environment.GetEnvironmentVariable("TWILIO_API_KEY"); //VideoChatAppApiKey
                    settings.AccountSid = Environment.GetEnvironmentVariable("SKbca8712511b92b145de7921da52c3e7d");
                    settings.ApiSecret = Environment.GetEnvironmentVariable("1uBK4bJrP2VNUxJwzdeIhKPVfAdSqwPe"); 
                    settings.ApiKey = Environment.GetEnvironmentVariable("VideoChatAppApiKey");
                })
                .AddTransient<IVideoService, VideoService>()
                .AddSpaStaticFiles(config => config.RootPath = "ClientApp/dist");

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });

        }
    }
}
