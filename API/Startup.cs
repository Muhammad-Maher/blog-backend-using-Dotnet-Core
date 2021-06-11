using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ADDED NAMESPACES
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Repository;
//

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// FOR CORS
        string MyAllowSpecificOrigins = "x";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region DEPENDENCY INJECTION (INJECTED SERVICES)

            /// INJECTED SERVICES 
            /// USE LAZY LOADING
            services.AddDbContext<Context>(option => option.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("mycon")));

            /// LOOP HANDLING
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            /// CORS POLICY
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();

                });
            });

            /// FOR REPOSITORY 
            services.AddScoped<ICommentRepository, commentRepository>();
            services.AddScoped<IPostRepository, postRepository>();
            /// 

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API for blogPost", Version = "v1" });
            });


           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                    c.RoutePrefix = string.Empty;
                }
                );
            

            // ADDED 
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            // ADDED
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            }
            );
        }   
    }
}



