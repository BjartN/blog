using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Blog.Core;
using Blog.Infrastructure.RSS;
using Module=Autofac.Module;
using Blog.Infrastructure.MongoDb;

namespace Blog.Web
{
    public class BootstrapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DefaultConvention.ApplyDefaultConvention(builder, typeof(IRepository).Assembly, typeof(ISyndicationService).Assembly);

            builder
                //.Register(x => new Repository(HttpContext.Current.Server.MapPath("~/App_Data/Db4o.yap")))
                .Register(x=> new MongoRepository("blog"))
                .As <IRepository>()
                .SingleInstance();
        }
    }

    /// <summary>
    /// Let {Name} be default implementaion for interface I{Name} to remove
    /// repetative registration.
    /// </summary>
    class DefaultConvention
    {
        public static void ApplyDefaultConvention(ContainerBuilder builder, params Assembly[] assemblies)
        {
            var allTypes = mergeAssemblyTypes(assemblies);
            var interfaceTypes = allTypes.Where(t => t.IsInterface);

            foreach (var interfaceType in interfaceTypes)
            {
                var type = allTypes.Where(t => interfaceType.IsAssignableFrom(t) && "I" + t.Name == interfaceType.Name).SingleOrDefault();
                if (type != null)
                    builder.RegisterType(type).As(interfaceType);
            }
        }

        private static List<Type> mergeAssemblyTypes(IEnumerable<Assembly> assemblies)
        {
            var types = new List<Type>();
            foreach (var a in assemblies)
                types.AddRange(a.GetTypes());
            return types;
        }
    }
}