using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Config;
using TodoApi.Services;
using Microsoft.Extensions.Options;

namespace TodoApi
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
            services.AddDbContext<TodoContext>(opt =>
               opt.UseInMemoryDatabase("TodoList"));
            services.AddControllers();
            // PER IGNORARE NEL model-binding DIFFERENZA TRA nullable E non-nullable - BISOGNA USARE ESPLICITAMENTE [Required]
            // services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            // PER USARE VECCHIO JSONET Add a package reference to Microsoft.AspNetCore.Mvc.NewtonsoftJson
            // services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
            // QUESTO RIABILITA PascalCase NEL JSON + POSSO USARE SUI MODEL [JsonProperty("Name")] using Newtonsoft.Json;

            // requires using Microsoft.Extensions.Options to read Config (appsettings.json)
            services.Configure<MyDatabaseSettings>(
                    Configuration.GetSection(nameof(MyDatabaseSettings)));
            services.AddSingleton<IMyDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MyDatabaseSettings>>().Value);
            // sample Repository pattern + Config
            services.AddSingleton<ITodoRepository, TodoRepository>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
