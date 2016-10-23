using _4Mat.Data.External;
using _4Mat.Processing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _4Mat.Processing.Services
{
    public class ModuleService : IModuleService
    {
        public const int MAX_RETRIES = 2;

        public static class ModuleNames
        {
            public static string Employer = "Employer";
        }

        public Module LoadModule(Guid apiKey, string moduleName)
        {
            //DC677876-062A-4FC6-AD1D-7494B70FE8C1 
            //6A9393B4-DBF1-4628-BD69-410D5DD4BF82

            //Assumes we only care about the FIRST module
            return LoadModules(apiKey)
                .FirstOrDefault(x => x.Name.Equals(moduleName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Module> LoadModules(Guid apiKey)
        { 
            List<Module> modules = null;
            var client = new HttpClient();

            int attempts = 0;

            while (modules == null)
            {
                try
                {
                    var url = GenerateUrl(apiKey);
                    var task = client.GetAsync(url)
                      .ContinueWith((awaitResult) =>
                      {
                          var response = awaitResult.Result;
                          if (!response.IsSuccessStatusCode) throw new Exception("Retry");

                          var jsonString = response.Content.ReadAsStringAsync();
                          jsonString.Wait();
                          modules = JsonConvert.DeserializeObject<List<Module>>(jsonString.Result);

                      });
                    task.Wait();
                } 
                catch (Exception)
                {
                    attempts += 1;
                    if (attempts > MAX_RETRIES) throw;
                }
            }
            return modules;

        }

        private string GenerateUrl(Guid apiKey)
        {
            return string.Format("https://dv3api.service.4matnetworks.com/api/Module/GetModules?apiKey={0}", apiKey);
        }
    }
}
