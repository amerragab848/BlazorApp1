using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorApp1.Areas.Identity;
using BlazorApp1.Services;
using System.Net.Http;
using System.Security.Authentication;
using BlazorApp1.Data;
using AutoMapper;
using BlazorApp1.AutoMapper;
using Blazored.LocalStorage;

namespace BlazorApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
           // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
           // .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddRazorPages();
            services.AddServerSideBlazor(options => options.JSInteropDefaultCallTimeout = TimeSpan.FromSeconds(2000));
            services.AddBlazoredLocalStorage();
            services.AddAuthorizationCore();
          //  services.AddScoped<ApiAuthenticationStateProvider>();
          //  services.AddScoped<AuthenticationStateProvider>(provider =>
           // provider.GetRequiredService<ApiAuthenticationStateProvider>());

             services.AddScoped<ToastService>();

              services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            if (!services.Any(x => x.ServiceType == typeof(System.Net.Http.HttpClient)))
            {
                
                services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44330/");
                })
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
                    SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
                });

            }
            if (!services.Any(x => x.ServiceType == typeof(System.Net.Http.HttpClient)))
            {

                services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44330/");
                })
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
                    SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
                });

            }
            if (!services.Any(x => x.ServiceType == typeof(System.Net.Http.HttpClient)))
            {

                services.AddHttpClient<IAuthService, AuthService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44330/");
                })
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
                    SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12
                });

            }


            services.AddAutoMapper(typeof(EmployeeProfile));
            services.AddProtectedBrowserStorage();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

          //  app.UseAuthentication();
          //  app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
