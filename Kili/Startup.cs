using Kili.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kili
{
    public class Startup
    {    
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            public void ConfigureServices(IServiceCollection services)
        {
            //Ajout le service de gestion des sessions dans le parcours de vente en ligne entre autres
            services.AddSession();

            //Ajoute le service d'authentification par cookies
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                 options.LoginPath = "/Login/Authentification";

             });


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (BddContext _bddContext = new BddContext()) {

                _bddContext.InitializeDb();

        }
            /*using (var ctx = new BddContext())
            {
                ctx.InitializeDb();
            }*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            //AJoute les fonctionnalités d'authentification
            app.UseAuthentication();
            app.UseAuthorization();

            //Ajoute les fonctionnalités de sessions
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
