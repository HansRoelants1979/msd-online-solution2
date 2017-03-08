﻿using Microsoft.Practices.Unity;
using Newtonsoft.Json.Converters;
using System.Web.Http;
using Tc.Crm.Service.Resolver;
using Tc.Crm.Service.Services;

namespace Tc.Crm.Service
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api")]
    public static class WebApiConfig
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IBookingService, BookingService>();
            container.RegisterType<ICrmService, CrmService>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

    }
}