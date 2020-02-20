using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        /* The constructor receives the configuration data loaded from the appsettings.json file, which is presented through an object that implements the IConfiguration interface.  */
        /* The constructor assigns the IConfiguration object to a property called Configuration so that it can be used by the rest of the Startup class. */
        public Startup(IConfiguration configuration) =>
             Configuration = configuration;
        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /* The AddDbContext extension method sets up the services provided by Entity Framework Core for the database context class  The argument to the AddDbContext method is a lambda expression that receives an options object that
                configures the database for the context class.*/
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreProducts:ConnectionString"]));

            /*  when a component,  such as a controller, needs an implementation of the IProductRepository interface, it should receive an instance of the FakeProductRepository class. 
                The AddTransient method specifies that a new FakeProductRepository object should be created each time the IProductRepository interface is needed. */
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /* The Configure method of the Startup class is used to set up the request pipeline, which consists of
        classes(known as middleware) that will inspect HTTP requests and generate responses.The UseMvc method
       sets up the MVC middleware, and one of the configuration options is the scheme that will be used to map
       URLs to controllers and action methods. 
       */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // This extension method displays details of exceptions that occur in the application
            app.UseDeveloperExceptionPage();
            // This extension method adds a simple message to HTTP responses that would not otherwise have a body, such as 404
            app.UseStatusCodePages();
            // This extension method enables support for serving static content from  the wwwroot folder
            app.UseStaticFiles();
            //This extension method enables ASP.NET Core MVC
            // I need to tell MVC that it should send requests that arrive for the root URL of my application (http:// mysite /) to the List action method in the ProductController class.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "pagination",
                 template: "Products/Page{productPage}",
                 defaults: new { Controller = "Product", action = "List" });
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
