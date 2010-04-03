using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace TumblrImport
{
    public static class RouteHelpers
    {
        public static void AssertRoute(RouteCollection routes, string url, object expectations)
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns(url);

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            Assert.IsNotNull(routeData, "Should have found the route");

            foreach (PropertyValue property in GetProperties(expectations))
            {
                Assert.IsTrue(
                    string.Equals(property.Value.ToString(),routeData.Values[property.Name].ToString(), StringComparison.OrdinalIgnoreCase), 
                    string.Format("Expected '{0}', not '{1}' for '{2}'.",property.Value, routeData.Values[property.Name], property.Name));
            }
        }

        private static IEnumerable<PropertyValue> GetProperties(object o)
        {
            if (o != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
                    }
                }
            }
        }

        private class PropertyValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}