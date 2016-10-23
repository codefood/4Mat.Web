using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Data.External
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CachedTime { get; set; }
    }
}
