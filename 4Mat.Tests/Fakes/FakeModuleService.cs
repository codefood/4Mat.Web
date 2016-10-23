using _4Mat.Processing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4Mat.Data.External;

namespace _4Mat.Tests.Fakes
{
    public class FakeModuleService : IModuleService
    {
        public Module LoadModule(Guid apiKey, string moduleName)
        {
            return new Module
            {
                Name = moduleName,
                IsActive = true
            };
        }
    }
}
