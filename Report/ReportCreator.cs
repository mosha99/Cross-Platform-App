//using Stimulsoft.Base;
//using Stimulsoft.Report;
//using Stimulsoft.Report.Components;

using Stimulsoft.Base.Licenses;
using Stimulsoft.System.Security.Cryptography;
using System.Reflection;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Newtonsoft.Json;
using Stimulsoft.Base.Plans;
using System.ComponentModel;
using System.Text;
using Stimulsoft.Base.Helpers;
using System.Security.Cryptography;
using RSACryptoServiceProvider = Stimulsoft.System.Security.Cryptography.RSACryptoServiceProvider;
using SHA1CryptoServiceProvider = Stimulsoft.System.Security.Cryptography.SHA1CryptoServiceProvider;

namespace Report
{
    public class ReportCreator
    {
        private const string key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkgpgFGkUl79uxVs8X+uspx6K+tqdtOB5G1S6PFPRrlVNvMUiSiNYl724EZbrUAWwAYHlGLRbvxMviMExTh2l9xZJ2xc4K1z3ZVudRpQpuDdFq+fe0wKXSKlB6okl0hUd2ikQHfyzsAN8fJltqvGRa5LI8BFkA/f7tffwK6jzW5xYYhHxQpU3hy4fmKo/BSg6yKAoUq3yMZTG6tWeKnWcI6ftCDxEHd30EjMISNn1LCdLN0/4YmedTjM7x+0dMiI2Qif/yI+y8gmdbostOE8S2ZjrpKsgxVv2AAZPdzHEkzYSzx81RHDzZBhKRZc5mwWAmXsWBFRQol9PdSQ8BZYLqvJ4Jzrcrext+t1ZD7HE1RZPLPAqErO9eo+7Zn9Cvu5O73+b9dxhE2sRyAv9Tl1lV2WqMezWRsO55Q3LntawkPq0HvBkd9f8uVuq9zk7VKegetCDLb0wszBAs1mjWzN+ACVHiPVKIk94/QlCkj31dWCg8YTrT5btsKcLibxog7pv1+2e4yocZKWsposmcJbgG0";

        public ReportCreator()
        {
            //var stream = new MemoryStream(ReportResource.license);
            //StiLicense.LoadFromStream(stream);

            StiLicense.Key = key;
        }
        public Stream CreateReport(Stream stream, byte[] reportFile, params KeyValuePair<string, object>[] businessObjects)
        {
            try
            {     
                StiReport report = new StiReport();
                report.Load(reportFile);

                if (businessObjects != null)
                    foreach (var bo in businessObjects)
                    {
                        report.RegData(bo.Key, bo.Value);
                        //report.RegBusinessObject(bo.Key,bo.Value);
                    }

                report.Render(false);
                report.ExportDocument(StiExportFormat.Pdf, stream);
                return stream;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

    }
}