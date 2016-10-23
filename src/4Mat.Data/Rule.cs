using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Data
{
    public class Rule
    {
        
        public Rule() : this(string.Empty, true) { }
        public Rule(string fieldName) : this(fieldName, true) { }

        public Rule(string fieldName, bool required)
        {
            FieldName = fieldName;
            Required = required;
        }

        public string FieldName { get; set; }
        public bool Required { get; set; }
    }
}
