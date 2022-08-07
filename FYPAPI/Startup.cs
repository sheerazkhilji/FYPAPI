using API.DBManager;
using API.IServices;
using API.Services;
using FYPAPI.IServices;
using FYPAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FYPAPI
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

            services.AddControllers();




            services.AddCors(options =>
            {
                options.AddPolicy("defaultcorspolicy", builder =>
                {
                    //builder.WithOrigins("*").WithHeaders("*").WithMethods("*");

                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                }
                    );


            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FYPAPI", Version = "v1" });
            });


            services.AddTransient<IDapper, Dapperr>();

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();

            services.AddTransient<IUserServices, UserServices>();

            services.AddTransient<IVendorServices, VendorServices>();

            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IorderServices, orderServices>();



           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FYPAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("defaultcorspolicy");
            app.UseRouting();
            app.UseStaticFiles();
            StaticFileOptions options = new StaticFileOptions { ContentTypeProvider = new FileExtensionContentTypeProvider() }; ((FileExtensionContentTypeProvider)options.ContentTypeProvider).Mappings.Add(new KeyValuePair<string, string>(".glb", "model/gltf-buffer"));
            app.UseStaticFiles(options);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
