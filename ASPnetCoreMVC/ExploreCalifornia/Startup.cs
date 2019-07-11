using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration; //read like 'dictionary'
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FormattingService>();

            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            { //dependency injection logic
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
                //Singleton, share instances; entire lifetime.. to share common data to all users
                //Scoped, only one instance for each web request.. only one state!
                //Transient, shortest lifespan; new instance each time
            });

            services.AddDbContext<BlogDataContext>(options =>//For Auth (3)
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString); //connection to sql server
            });

            services.AddDbContext<IdentityDataContext>(options => //For Auth (4)
            {
                var connectionString = configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>() //For Authorization!! (2)
                .AddEntityFrameworkStores<IdentityDataContext>();

            services.AddMvc();
        }

        //new controller = new class...

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            FeatureToggles features)
        {
            app.UseExceptionHandler("/error.html");//logs error and redirects user for security measures, etc.
            //above won't be displayed while in development mode

            if (/*env.IsDevelopment() or.. configuration.GetValue<bool>("EnableDeveloperExceptions") or..*/features.DeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();//makes exceptions easier to handle
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });

            /****************************************************************************************/

            /*app.Use(async (context, next) => //request, next_middleware_function
            {
                if (context.Request.Path.Value.StartsWith("/hello"))//to check for a certain path
                {
                    await context.Response.WriteAsync("Will make app.Run work as well.. ");
                }
                await next();//when next task should execute
            });

            app.Run(async (context) => //executes for every request -> https://.../whatever
            {
                await context.Response.WriteAsync("Hello World!");
            });*/

            /****************************************************************************************/


            app.UseIdentity();//for Authorization! (1)

            app.UseMvc(routes => //for defining route patterns
            {
                routes.MapRoute("Default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseFileServer();
        }
    }
}
