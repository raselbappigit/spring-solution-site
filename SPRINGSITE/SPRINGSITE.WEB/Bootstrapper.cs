using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SPRINGSITE.DATA;
using System.Web.Http;
using Autofac;
using System.Reflection;
using SPRINGSITE.SERVICE;
using Autofac.Integration.Mvc;

namespace SPRINGSITE.WEB
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            InitializeAndSeedDb();
            SetIocContainer();
        }

        private static void InitializeAndSeedDb()
        {
            // Initializes and seeds the database.
            Database.SetInitializer(new DBInitializer());

            using (var context = new AppDbContext())
            {
                context.Database.Initialize(force: true);
            }
        }

        private static void SetIocContainer()
        {
            //Implement Autofac

            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            // Register MVC controllers using assembly scanning.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Register MVC controller and API controller dependencies per request.
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            // Register service
            builder.RegisterAssemblyTypes(typeof(ProfileService).Assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces().InstancePerDependency();

            // Register repository
            builder.RegisterAssemblyTypes(typeof(ProfileRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerDependency();

            var container = builder.Build();

            //for MVC Controller Set the dependency resolver implementation.
            var resolverMvc = new AutofacDependencyResolver(container);
            System.Web.Mvc.DependencyResolver.SetResolver(resolverMvc);

        }

    }
}