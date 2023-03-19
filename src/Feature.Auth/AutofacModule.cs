using Autofac;
using Feature.Auth.Business;
using Feature.Auth.Config;
using Feature.Auth.Controllers;
using Feature.Auth.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth
{
    public class AutofacModule: Autofac.Module
    {
        public AuthConfig AuthConfig{ get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            RepoDb.SqlServerBootstrap.Initialize();
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();

            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            builder.RegisterInstance(httpClientFactory);

            builder.RegisterType(typeof(AuthRepository))
              .InstancePerDependency();

            //builder.RegisterAssemblyTypes(typeof(AuthRepository).GetTypeInfo().Assembly)
            //   .Where(t => t.Name.EndsWith("Repository"))
            //   .AsImplementedInterfaces()
            //   .InstancePerLifetimeScope();

            builder.RegisterType(typeof(AuthBusiness))
             .InstancePerDependency();

            //builder.RegisterAssemblyTypes(typeof(AuthBusiness).GetTypeInfo().Assembly)
            //   .Where(t => t.Name.EndsWith("Business"))
            //   .AsImplementedInterfaces()
            //   .InstancePerLifetimeScope();

            var dbConnection = new System.Data.SqlClient.SqlConnection(AuthConfig.ConnectionString);
            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .WithProperty("DbConnection", dbConnection)
                .InstancePerDependency();

            //builder.RegisterType(typeof(AuthBusiness))
            // .InstancePerDependency();

            //builder.RegisterAssemblyTypes(typeof(AuthController).GetTypeInfo().Assembly)
            //   .Where(t => t.Name.EndsWith("Controller"))
            //   .AsImplementedInterfaces()
            //   .InstancePerLifetimeScope();

        }
    }
}
