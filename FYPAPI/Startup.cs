using API.DBManager;
using API.IServices;
using API.Services;
using ClassLibrary1;
using FYPAPI.IServices;
using FYPAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stripe;
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

            services.Configure<IISServerOptions>(options => {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.BufferBodyLengthLimit = int.MaxValue;
                x.MultipartBoundaryLengthLimit = int.MaxValue;
            });


            services.AddCors(options =>
            {
                options.AddPolicy("defaultcorspolicy", builder =>
                {
                    //builder.WithOrigins("*").WithHeaders("*").WithMethods("*");

                    builder.AllowAnyOrigin()
                    .AllowAnyMethod().
                    AllowAnyHeader().WithExposedHeaders("content-disposition");

                }
                    );


            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FYPAPI", Version = "v1" });
            });
            StripeConfiguration.ApiKey = Configuration.GetValue<string>("StripeSettings:SecretKey");


            services.AddTransient<IDapper, Dapperr>();

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();

            services.AddTransient<IUserServices, UserServices>();

            services.AddTransient<IVendorServices, VendorServices>();

            services.AddTransient<ICategoryServices, CategoryServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IorderServices, orderServices>();
            services.AddTransient<IDashboardServices, DashboardServices>();

            services.AddTransient<CustomerService>()
                .AddTransient<ChargeService>()
                .AddTransient<TokenService>()
                .AddTransient<IStripeAppService, StripeAppService>();




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
