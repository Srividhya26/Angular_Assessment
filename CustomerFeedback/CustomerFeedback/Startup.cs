using CustomerFeedbackBL.Interfaces;
using CustomerFeedbackBL.Logic;
using CustomerFeedbackDA.Data;
using CustomerFeedbackDA.Interfaces;
using CustomerFeedbackDA.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using NLog.Web.AspNetCore;
using NLog.Extensions.Logging;

namespace CustomerFeedback
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerFeedback", Version = "v1" });
            });

            services.AddDbContext<CustomerFeedbackContext>
              (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFeedbackBLRepo, FeedbackBLRepo>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerFeedback v1"));
            }

            

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
