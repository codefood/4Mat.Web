using _4Mat.Data.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Processing.Interfaces
{
    public interface IModuleService
    {
        Module LoadModule(Guid apiKey, string moduleName);
    }
}
