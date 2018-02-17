using Microsoft.Research.Science.FetchClimate2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standalone
{
    public class StaticConfiguration
    {
        public static IVariableDefinition[] ActiveVariables = {
            new VariableDefinition("airt","Degrees C","Air temperature near surface"),
            new VariableDefinition("abshum","g/m^3","Absolute air humidity"),
            new VariableDefinition("airt_land","Degrees C","Air temperature near surface (land only area)"),
            new VariableDefinition("airt_ocean","Degrees C","Air temperature near surface (ocean only area)"),
            new VariableDefinition("depth_ocean","meters","Depth below sea level (ocean only area)"),
            new VariableDefinition("dtr","Degrees C","Diurnal air temperature rate"),
            new VariableDefinition("elev","meters","Elevation above sea level"),
            new VariableDefinition("elev_land","meters","Elevation above sea level (land only area)"),
            new VariableDefinition("frs","days/month","Frost days frequency"),
            new VariableDefinition("pet","mm/month","Potential evapotranspiration"),
            new VariableDefinition("prate","mm/month","Precipitation rate"),
            new VariableDefinition("relhum","percentage","Relative humidity"),
            new VariableDefinition("relhum_land","percentage","Relative humidity (land only area)"),
            new VariableDefinition("soilmoist","mm/m","Soil moisture"),
            new VariableDefinition("sunp","Percent of maximum possible sunshine","Sunshine fraction"),
            new VariableDefinition("wet","days/month","Wet days frequency"),
            new VariableDefinition("windspeed","m/s","Wind speed at 10m"),
            new VariableDefinition("wvp","hPa","Water vapour pressure"),
            new VariableDefinition("wvsp","hPa","Water vapour saturation pressure")
            };

        private static string GetDataSetURL(string filename) {
            return string.Format(@"msds:nc?file=C:\Users\dmitr\Desktop\fc_dump\blobs\net-cdf\{0}&openMode=readOnly", filename);
        }

        public static LocalDataSourceDefinition[] GetDataSources()
        {
            Dictionary<string, string> cruEnv2DsNameMappings = new Dictionary<string, string>();
            cruEnv2DsNameMappings.Add("airt", "tmp");
            cruEnv2DsNameMappings.Add("prate", "pre");
            cruEnv2DsNameMappings.Add("relhum", "reh");
            cruEnv2DsNameMappings.Add("dtr", "dtr");
            cruEnv2DsNameMappings.Add("frs", "frs");
            cruEnv2DsNameMappings.Add("wet", "rd0");
            cruEnv2DsNameMappings.Add("sunp", "sunp");
            cruEnv2DsNameMappings.Add("windspeed", "wnd");
            cruEnv2DsNameMappings.Add("relhum_land", "reh");
            cruEnv2DsNameMappings.Add("airt_land", "tmp");

            Dictionary<string,string> reanalysisTempEnv2DsNameMappings = new Dictionary<string, string>();
            reanalysisTempEnv2DsNameMappings.Add("airt","air");
            reanalysisTempEnv2DsNameMappings.Add("airt_land", "air");

            Dictionary<string, string> worldclimEnv2DsNameMappings = new Dictionary<string, string>();
            worldclimEnv2DsNameMappings.Add("airt", "tmean");
            worldclimEnv2DsNameMappings.Add("airt_land", "tmean");
            worldclimEnv2DsNameMappings.Add("prate", "prec");

            LocalDataSourceDefinition[] ActiveDataSources = {
            new LocalDataSourceDefinition(
                1,
                "CRU CL 2.0",
                "High-resolution grid of the average climate in the recent past",
                "Produced by Climatic Research Unit (University of East Anglia). http://www.cru.uea.ac.uk",
                GetDataSetURL("cru2_wo_strings_with_variograms.nc"),
                "Microsoft.Research.Science.FetchClimate2.DataSources.CruCl20DataHandler, CRUCL2DataSource, Version=2.1.20342.0, Culture=neutral, PublicKeyToken=null",
                new string[] { "airt","prate","relhum","dtr","frs","wet","sunp","windspeed","relhum_land","airt_land"},
                cruEnv2DsNameMappings
            ),
            new LocalDataSourceDefinition(
                 6,
                "NCEP/NCAR Reanalysis 1 (regular grid)",
                "The NCEP/NCAR Reanalysis 1 project is using a state-of-the-art analysis/forecast system to perform data assimilation using past data from 1948 to the present",
                "NCEP Reanalysis data provided by the NOAA/OAR/ESRL PSD, Boulder, Colorado, USA, from their Web site at http://www.esrl.noaa.gov/psd/",
                GetDataSetURL("ReanalysisRegular_with_variograms.nc"),
                "Microsoft.Research.Science.FetchClimate2.DataSources.NCEPReanalysisRegularGridDataHandler, NCEPReanalysisDataSource, Version=2.1.20337.0, Culture=neutral, PublicKeyToken=null",
                new string[] { "airt" },
                reanalysisTempEnv2DsNameMappings
                ),
            new LocalDataSourceDefinition(
                2,
                "WorldClim 1.4",
                "A set of global climate layers (climate grids) with a spatial resolution of a square kilometer",
                "The database is documented in this article: Hijmans, R.J., S.E. Cameron, J.L. Parra, P.G. Jones and A. Jarvis, 2005. Very high resolution interpolated climate surfaces for global land areas. International Journal of Climatology 25: 1965-1978.",
                GetDataSetURL("WorldClimCurr_with_variograms.nc"),
                "Microsoft.Research.Science.FetchClimate2.WorldClim14DataSource, WorldClim14DataSource, Version=2.1.20342.0, Culture=neutral, PublicKeyToken=null",
                new string[] { "airt","prate","airt_land"},
                worldclimEnv2DsNameMappings
                )
                /*(
            new ExtendedDataSourceDefinition(
                2,
                "WorldClim 1.4",
                "A set of global climate layers (climate grids) with a spatial resolution of a square kilometer",
                "The database is documented in this article: Hijmans, R.J., S.E. Cameron, J.L. Parra, P.G. Jones and A. Jarvis, 2005. Very high resolution interpolated climate surfaces for global land areas. International Journal of Climatology 25: 1965-1978.",
                "http://fcdatasets.blob.core.windows.net/net-cdf/WorldClimCurr_with_variograms.nc",
                "Microsoft.Research.Science.FetchClimate2.WorldClim14DataSource, WorldClim14DataSource, Version=2.1.20342.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                3,
                "GHCNv2",
                "The Global Historical Climatology Network (GHCN-Monthly) data base contains historical temperature, precipitation, and pressure data for thousands of land stations worldwide.",
                "http://www.ncdc.noaa.gov/ghcnm/v2.php",
                "http://fcdatasets.blob.core.windows.net/net-cdf/GHCNv2_201107_wo_strings.nc",
                "GHCNv2DataSource.DataHandler, GHCNv2DataSource, Version=2.1.20342.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                4,
                "GTOPO30",
                "GTOPO30 is a global digital elevation model (DEM) with a horizontal grid spacing of 30 arc seconds (approximately 1 kilometer). GTOPO30, completed in late 1996, was developed over a three year period through a collaborative effort led by staff at the U.S. Geological Survey's Center for Earth Resources Observation and Science (EROS).",
                "http://eros.usgs.gov/#/Find_Data/Products_and_Data_Available/gtopo30_info",
                "http://fcdatasets.blob.core.windows.net/net-cdf/GTOPO30_with_variograms.nc",
                "GTOPO30DataSource.GTOPO30DataHandler, GTOPO30DataSource, Version=2.1.20342.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                5,
                "ETOPO1",
                "ETOPO1 is a 1 arc-minute global relief model of Earth's surface that integrates land topography and ocean bathymetry. It was built from numerous global and regional data sets. The service uses the version depicting the top of the Antarctic and Greenland ice sheets",
                "Amante, C. and B. W. Eakins, ETOPO1 1 Arc-Minute Global Relief Model: Procedures, Data Sources and Analysis. NOAA Technical Memorandum NESDIS NGDC-24, 19 pp, March 2009. http://www.ngdc.noaa.gov/mgg/global/global.html",
                "http://fcdatasets.blob.core.windows.net/net-cdf/ETOPO1_Ice_g_gmt4_with_variograms.nc",
                "ETOPO1DataSource.ETOPO1DataHandler, ETOPO1DataSource, Version=2.1.20342.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
               
            ),
            new ExtendedDataSourceDefinition(
                7,
                "NCEP/NCAR Reanalysis 1 (Gauss T62)",
                "The NCEP/NCAR Reanalysis 1 project is using a state-of-the-art analysis/forecast system to perform data assimilation using past data from 1948 to the present",
                "NCEP Reanalysis data provided by the NOAA/OAR/ESRL PSD, Boulder, Colorado, USA, from their Web site at http://www.esrl.noaa.gov/psd/",
                "http://fcdatasets.blob.core.windows.net/net-cdf/ReanslysisGaussT62_with_variograms.nc",
                "Microsoft.Research.Science.FetchClimate2.DataSources.NCEPReanalysisGaussT62GridDataHandler, NCEPReanalysisDataSource, Version=2.1.20337.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                8,
                "CpcSoilMoisture",
                "The monthly data set consists of a file containing monthly averaged soil moisture water height equivalents. Note that data is model-calculated and not measured directly. The dataset is now V2. There are some differences with the previous version, particularly over Africa. The V2 version also has the landmask applied to the datavalues",
                "Soil Moisture data provided by the NOAA/OAR/ESRL PSD, Boulder, Colorado, USA, from their Web site at http://www.esrl.noaa.gov/psd/",
                "http://fcdatasets.blob.core.windows.net/net-cdf/soilw.mon.mean.v2_with_variograms.nc",
                "CPCDataSource.CpcDataHandler, CPCDataSource, Version=2.1.20337.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                9,
                "Malmstrom PET",
                "Monthly potential evapotranspiration according to Malmstrom VH (1969)",
                "Malmstrom VH (1969) A new approach to the classification of climate. J Geog 68:351-357.",
                "",
                "MalmstromPET.FunctionClass, MalmstromPET, Version=2.1.20301.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                10,
                "WagnerWVSP",
                "W. Wagner et al. The IAPWS Formulation 1995 for the Thermodynamics Properties of Ordinary Water Substance for General and Scientific use",
                "Journal of Physical and Chemcal Reference data, June 2000, Volume 31, Issue 2",
                "",
                "WaterVapourSatPressure.FunctionClass, WagnerWVSP, Version=2.1.20300.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                11,
                "FC1 Variables",
                "Virtual support for FC1 variables FC_OCEAN_DEPTH, FC_LAND_RELATIVE_HUMIDITY, FC_OCEAN_AIR_TEMPERATURE.",
                "Microsoft Research",
                "",
                "FC1LegacySupport.LegacyVariables, FC1LegacySupport, Version=2.1.20300.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                12,
                "CESM1-BGC prate",
                "CESM1-BGC model output prepared for CMIP5 rcp85",
                "","http://fcdatasets.blob.core.windows.net/net-cdf/pr_day_CESM1-BGC_rcp85_r1i1p1_with_variograms_time_coerced.nc",
                "Microsoft.Research.Science.FetchClimate2.DataSources.CESM1BGCDataHandler, CESM1BGCDataSource, Version=2.1.20337.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            ),
            new ExtendedDataSourceDefinition(
                13,
                "CESM1-BGC airt",
                "CESM1-BGC model output prepared for CMIP5 rcp85",
                "", "http://fcdatasets.blob.core.windows.net/net-cdf/tas_day_CESM1-BGC_rcp85_r1i1p1_with_variograms_time_coerced.nc",
                "Microsoft.Research.Science.FetchClimate2.DataSources.CESM1BGCDataHandler, CESM1BGCDataSource, Version=2.1.20337.0, Culture=neutral, PublicKeyToken=null",
                null,
                null,
                null,
                null,
                0
            )*/
            };

            return ActiveDataSources;
        }
    }

    public class StaticExtendedConfigurationProvider : IExtendedConfigurationProvider
    {
        private readonly DateTime confTime = new DateTime(2018, 1, 1);

        private static ExtendedDataSourceDefinition LocalToExtended(LocalDataSourceDefinition local) {
            return new ExtendedDataSourceDefinition(
                local.ID,
                local.Name,
                local.Description,
                local.Copyright,
                local.Uri,
                local.HandlerTypeName,
                local.ProvidedVariables,
                null,
                local.EnvToDsMapping,
                null,
                0
                );
        }

        public ExtendedConfiguration GetConfiguration(DateTime utcTime)
        {
            var dataSources = StaticConfiguration.GetDataSources().Select(local => LocalToExtended(local)).ToArray();
            return new ExtendedConfiguration(confTime, dataSources, StaticConfiguration.ActiveVariables);
        }

        public DateTime GetExactTimestamp(DateTime utcTimestamp)
        {
            return confTime;
        }
    }
}
