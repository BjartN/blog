using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;

namespace Blog.Web
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");

            routes.IgnoreRoute("{*favicon}", new
            {
                favicon =
                    @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?"
            });

            routes.MapRoute(
               "Legacy routes",                                            
               "post/{id}/{slug}",
                new { controller = "Post", action = "Index", slug="" },
                new { id = new IntegerIdContraint()}
            );

            routes.MapRoute(
                "Slug",                                                 
                "post/{slug}",                                               
                new { controller = "Post", action = "GetPostBySlug" }
                );

            routes.MapRoute(
             "Tags",
             "tag/{tag}",
              new { controller = "Post", action = "PostsByTag" }
            );

            routes.MapRoute(
                "Default",                                             
                "{controller}/{action}/{id}",                          
                new { controller = "Home", action = "Index", id = "" }  
                );
        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BootstrapperModule());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            _containerProvider = new ContainerProvider(builder.Build());

            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(_containerProvider));

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        private static ContainerProvider _containerProvider;
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }
    }

    public class IntegerIdContraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var value = values[parameterName].ToString();
            int someInt;
            return int.TryParse(value, out someInt);
        }
    }
}

