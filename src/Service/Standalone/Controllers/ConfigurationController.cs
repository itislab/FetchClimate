using Microsoft.Research.Science.FetchClimate2;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Standalone.Controllers {
    public class ConfigurationController : ApiController {
        

        // GET api/Configuration
        public Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration Get() {
            var dataSources = StaticConfiguration.GetDataSources();
            IFetchConfiguration configuration = new FetchConfiguration(DateTime.Now, dataSources, StaticConfiguration.ActiveVariables);
            var toSerizlize = new Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration(configuration);
            return toSerizlize;
        }

        // GET api/Configuration?timestamp=03-Nov-2012%2012:00:00
        public Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration Get(DateTime timestamp) {
            try {
                var dataSources = StaticConfiguration.GetDataSources();
                IFetchConfiguration configuration = new FetchConfiguration(DateTime.Now, dataSources, StaticConfiguration.ActiveVariables);
                var toSerizlize = new Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration(configuration);
                return toSerizlize;
            }
            catch (ArgumentException exc) {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    ReasonPhrase = exc.Message
                });
            }
        }
    }
}
