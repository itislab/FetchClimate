using Microsoft.Research.Science.FetchClimate2;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Standalone.Controllers
{
    public class ConfigurationController : ApiController
    {
        private static IVariableDefinition[] ActiveVariables = {
            new VariableDefinition("airt","Degrees C","Air temperature near surface")
        };

        private static IDataSourceDefinition[] ActiveDataSources = {
            new DataSourceDefinition(1,
                "Reanalysis",
                "The NCEP/NCAR Reanalysis 1 project is using a state-of-the-art analysis/forecast system to perform data assimilation using past data from 1948 to the present",
                "NCEP Reanalysis data provided by the NOAA/OAR/ESRL PSD, Boulder, Colorado, USA, from their Web site at http://www.esrl.noaa.gov/psd/",
                null, new string[] { "airt" })
        };

        private static IFetchConfiguration ActiveConfiguration =
            new FetchConfiguration(DateTime.UtcNow, ActiveDataSources, ActiveVariables);

        // GET api/Configuration
        public Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration Get()
        {            
            var toSerizlize = new Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration(ActiveConfiguration);
            return toSerizlize;
        }

        // GET api/Configuration?timestamp=03-Nov-2012%2012:00:00
        public Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration Get(DateTime timestamp)
        {
            try
            {                
                var toSerizlize = new Microsoft.Research.Science.FetchClimate2.Serializable.FetchConfiguration(ActiveConfiguration);
                return toSerizlize;
            }
            catch (ArgumentException exc)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) 
                { 
                    ReasonPhrase = exc.Message
                });
            }
        }
    }
}
