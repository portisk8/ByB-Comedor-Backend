using Autofac;
using Autofac.Extensions.DependencyInjection;
using Feature.Api.Config;
using Feature.Auth.Authentication;
using Feature.Auth.Config;
using Feature.Auth.Middleware;
using Feature.Core.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ByBComedor.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        public GeneralConfig GeneralConfig { get; set; }


        public Startup(IConfiguration configuration)
        {
            //Fix rename ClaimType.NameIdentifier by nameid (http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier)
            //Source: https://github.com/dotnet/aspnetcore/issues/4660
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            Configuration = configuration;
            GeneralConfig = new GeneralConfig
            {
                CorsUrls = Configuration["GeneralConfig:CorsUrls"]?.Split(";")
            };
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var allowedOrigins = GeneralConfig.CorsUrls;

            services.AddLogging();
            services.AddResponseCaching();

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builders =>
            {
                builders.WithOrigins(allowedOrigins.ToArray())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddMemoryCache();

            //Logger
            var logger = new LoggerConfiguration()
                                     .Enrich.WithExceptionDetails()
                                     .ReadFrom.Configuration(Configuration)
                                     .CreateLogger();
            services.AddSingleton<Serilog.ILogger>(logger);

            var authConfig = AuthConfig.Build(Configuration);
            var apiConfig = ApiConfig.Build(Configuration);
            
            //services.AddAuthentication("Bearer")
            //    .AddScheme<AuthenticationOptions, AuthenticationHandler>("Bearer", null);
            var key = Encoding.UTF8.GetBytes(authConfig.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ByBComedor", Version = "v1" });
            //    c.CustomSchemaIds(c => c.FullName);
            //});
            //Se registran los Configs de Features
            services.AddSingleton(authConfig);
            services.AddSingleton(apiConfig);

            // Create the container builder.
            var builder = new ContainerBuilder();

            //Se registran los Autofacs de Features
            builder.RegisterModule(new Feature.Auth.AutofacModule() { AuthConfig = authConfig });
            builder.RegisterModule(new Feature.Api.AutofacModule() { Config = apiConfig });

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);

        }

        public void Configure(IApplicationBuilder app,
                            IWebHostEnvironment env,
                            ILoggerFactory loggerFactory,
                            IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseResponseCaching();

            app.UseHttpsRedirection();

            //app.UseSwagger();

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ByBComedor.Api");
            //});

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapHub<TurnoHub>("/signalr/turnos")
                //    .RequireCors("TurneroCorsPolicy");
            });
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
