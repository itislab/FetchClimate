using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Microsoft.Research.Science.FetchClimate2.Tests
{
    static class TestConstants
    {
        public const double FloatPrecision = 1e-5;
        public const double DoublePrecision = 1e-13;


        public static readonly string UriCru;
        public static readonly string UriReanalysisRegular;
        public static readonly string UriReanalysisGauss;
        public static readonly string UriWorldClim;
        public static readonly string UriEtopo;
        public static readonly string UriGtopo;
        public static readonly string UriCpc;
        public static readonly string UriHADCM3_sra_tas;
        public static readonly string UriGHCN;

        public static readonly string CloudServiceURI = @"http://fetchclimate2.cloudapp.net";

        public static readonly string NetCDFDataSetsFolder = @"C:\Users\dmitr\Desktop\fc_dump\blobs\net-cdf";

        private static string FullDsPath(string relURI) {
            return System.IO.Path.Combine(NetCDFDataSetsFolder, relURI);
        }

        static TestConstants()
        {            
            UriCru = FullDsPath(@"cru2_wo_strings_with_variograms.nc?openMode=readOnly");
            UriReanalysisRegular = FullDsPath(@"ReanalysisRegular_with_variograms.nc?openMode=readOnly");
            UriReanalysisGauss = FullDsPath(@"ReanslysisGaussT62_with_variograms.nc?openMode=readOnly");
            UriWorldClim = FullDsPath(@"WorldClimCurr_with_variograms.nc?openMode=readOnly");
            UriEtopo = FullDsPath(@"ETOPO1_Ice_g_gmt4_with_variograms.nc?openMode=readOnly");
            UriGtopo = FullDsPath(@"GTOPO30_with_variograms.nc?openMode=readOnly");
            UriCpc = FullDsPath(@"soilw.mon.mean.v2_with_variograms.nc?openMode=readOnly");
            UriHADCM3_sra_tas = FullDsPath(@"HADCM3_SRA1B_1_N_tas_1-2399.nc?openMode=readOnly");
            UriGHCN = FullDsPath(@"GHCNv2_201107_wo_strings.nc?openMode=readOnly");
        }
    }
}
