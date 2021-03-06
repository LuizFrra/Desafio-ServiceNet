using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioServiceNetAPI.Infra.Context;
using DesafioServiceNetAPI.JWT;
using DesafioServiceNetAPI.JWT.Handler;
using DesafioServiceNetAPI.JWT.Keys;
using DesafioServiceNetAPI.Models;
using DesafioServiceNetAPI.Repository.Auth;
using DesafioServiceNetAPI.Repository.ClientR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MosaicoSolutions.ViaCep;
using Newtonsoft.Json;

namespace DesafioServiceNetAPI
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
            // CORS
            services.AddCors();
            // END CORS

            // BANCO DE DADOS
            services.AddDbContext<DesafioContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthRepository<User>, AuthRepository>();
            services.AddScoped<IClientRepository<Client>, ClientRepository>();
            // END BANCO DE DADOS

            // FACEBOOK
            services.Configure<Facebook.FaceBookSettings>(Configuration.GetSection("facebook"));
            services.AddSingleton<Facebook.FaceBookHandler>();
            // FIM FACEBOOK

            // TOKEN JWT
            services.AddSingleton<PublicKey>();
            services.AddSingleton<PrivateKey>();
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
            services.AddSingleton<IJwtHandler, JwtHandler>();
            
            var serviceProvider = services.BuildServiceProvider();
            var jwtParameters = serviceProvider.GetService<IJwtHandler>();
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = jwtParameters.parameters;
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
            });
            // END JWT TOKEN

            // VIACEP
            services.AddSingleton<IViaCepService>(serviceProvier => ViaCepService.Default());
            // END VIACEP
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
