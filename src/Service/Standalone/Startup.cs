﻿using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Standalone
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();

            // Use the extension method provided by the WebApi.Owin library:
            app.UseWebApi(webApiConfiguration);
        }


        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            
            config.Routes.MapHttpRoute(
                name: "Configuration",
                routeTemplate: "api/configuration/{timestamp}",
                defaults: new { controller = "Configuration", timestamp = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Compute",
                routeTemplate: "api/compute",
                defaults: new { controller = "Compute" }
            );

            config.Routes.MapHttpRoute(
                name: "Data",
                routeTemplate: "jsproxy/data/{uri}/{variables}",
                defaults: new { controller = "Proxy", uri = string.Empty, variables = string.Empty }
            );

            return config;
        }
    }
}
