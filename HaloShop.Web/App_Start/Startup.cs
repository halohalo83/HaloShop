using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HaloShop.Data;
using HaloShop.Data.Infrastructure;
using HaloShop.Data.Repositories;
using HaloShop.Service;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(HaloShop.Web.App_Start.Startup))]

namespace HaloShop.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigAutofac(app);
        }

        private void ConfigAutofac(IAppBuilder app)
        {
            var buider = new ContainerBuilder();
            buider.RegisterControllers(Assembly.GetExecutingAssembly());
            //Register Web API controllers
            buider.RegisterApiControllers(Assembly.GetExecutingAssembly());

            buider.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            buider.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            buider.RegisterType<HaloShopDbContext>().AsSelf().InstancePerRequest();

            // Repositories
            buider.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            buider.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            Autofac.IContainer container = buider.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);

        }
    }
}
