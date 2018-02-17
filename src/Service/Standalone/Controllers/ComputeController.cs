using Microsoft.Research.Science.Data;
using Microsoft.Research.Science.FetchClimate2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Standalone.Controllers
{
    /// <summary>Handler POST request to Compute endpoint</summary>
    public class ComputeController : ApiController
    {        
        public static readonly AutoRegistratingTraceSource ControllerTrace = new AutoRegistratingTraceSource("ComputeController");
        
        // GET api/Compute
        public string Get()
        {
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = "Compute endpoint doesn't support GET requests"
            });
        }

        public readonly string ResultsFolderPath = "Results";

        // POST api/Compute
        public string Post(Microsoft.Research.Science.FetchClimate2.Serializable.FetchRequest request)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                var fetchRequest = request.ConvertFromSerializable();

                string errorMsg;
                if (!fetchRequest.Domain.IsContentValid(out errorMsg)) //checking request content
                    return string.Format(Constants.FaultReply, errorMsg);

                string hash = fetchRequest.GetSHAHash();
                ControllerTrace.TraceInfo("{0}: Hash is computed for request", hash);

                string resultFilePath = Path.Combine(ResultsFolderPath, string.Format("{0}.nc", hash));
                string dsURI = string.Format("msds:nc?file={0}", resultFilePath);
                string dsReadURI = dsURI + "&openMode=readOnly";
                //cache lookup
                if (File.Exists(resultFilePath))
                {
                    sw.Stop();                   
                    return string.Format("completed={0}", dsReadURI);
                }

                

                var dataSources = StaticConfiguration.GetDataSources();
                IFetchConfiguration configuration = new FetchConfiguration(DateTime.Now, dataSources, StaticConfiguration.ActiveVariables);

                string fetchEngineTypeName = "Microsoft.Research.Science.FetchClimate2.FetchEngine, FetchEngine, Version=2.0.20339.0, Culture=neutral, PublicKeyToken=null";

                var feType = Type.GetType(fetchEngineTypeName);
                if (feType == null)
                    throw new InvalidOperationException("Cannot load fetch engine type " + feType);
                var feConst = feType.GetConstructor(new Type[1] { typeof(IExtendedConfigurationProvider) });
                if (feConst == null)
                    throw new InvalidOperationException("The FE constrictor with needed signature is not found. Are the currently running service assemblies and math assemblies from AzureGAC built with different Core assemblies?");
                IExtendedConfigurationProvider configProvider = new StaticExtendedConfigurationProvider();
                var fe = (IFetchEngine)feConst.Invoke(new object[1] { configProvider });
                var result = fe.PerformRequestAsync(fetchRequest).Result;

                if (!Directory.Exists(ResultsFolderPath))
                    Directory.CreateDirectory(ResultsFolderPath);
                
                
                string dsWriteURI = dsURI + "&openMode=create";
                
                RequestDataSetFormat.CreateCompletedRequestDataSet(dsWriteURI, fetchRequest, result.Values, result.Provenance, result.Uncertainty);
                sw.Stop();
                ControllerTrace.TraceInfo("Request {0} processed in {1}",hash,sw.Elapsed);
                return string.Format("completed={0}", dsReadURI);
            }
            catch (Exception exc)
            {
                ControllerTrace.TraceError("Request is processing error: {0}", exc.ToString());
                return string.Format("fault={0}",exc.ToString());
            }
        }
    }
}
