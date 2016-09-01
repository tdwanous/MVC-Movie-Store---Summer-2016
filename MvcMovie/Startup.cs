using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MvcMovie.Models.Identity;
using MvcMovie.Services;
using Owin;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(MvcMovie.Startup))]
namespace MvcMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<UserManager<ApplicationUser>>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerRequest();
            builder.RegisterType<ContextService>().As<IContextService>().InstancePerRequest();
            builder.RegisterType<OrderDetailService>().As<IOrderDetailService>().InstancePerRequest();
            builder.RegisterType<MailService>().As<IMailService>().InstancePerRequest();

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ConfigureAuth(app);
        }
    }
}
