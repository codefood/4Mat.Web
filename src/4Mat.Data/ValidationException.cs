using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Data
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
