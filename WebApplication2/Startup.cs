using System;
using ASP.NET.Sample.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ASP.NET.Sample.Web.Util;
using ASP.NET.Sample.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Sample.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseElmPage();
            app.UseElmCapture();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvcWithDefaultRoute();

            SampleData.Initialize(app.ApplicationServices);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MobileContext>(options => options.UseSqlServer(connection));

            services.AddOptions();
            services.Configure<Restrictions>(Configuration);

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieName = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });

            services.AddTransient<IMessageSender, EmailMessageSender>();

            // Add framework services.
            services.AddMvc();

            services.AddElm(options =>
            {
                options.Path = new PathString("/elm");
                options.Filter = (name, level) => level >= LogLevel.Error;
            });

            //services.Configure<MvcViewOptions>(options => {
            //    options.ViewEngines.Clear();
            //    options.ViewEngines.Insert(0, new CustomViewEngine());
            //});


        }
    }
}
