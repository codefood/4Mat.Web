using _4Mat.Processing.Interfaces;
using _4Mat.Processing.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4Mat.Data.External;
using _4Mat.Data;

namespace _4Mat.Tests.Fakes
{
    public class FakeValidator : IMessageValidator
    {
        public FakeValidator()
        {
            ValidationRules = new List<Rule>();
        }

        public IList<Rule> ValidationRules { get; set; }

        public void UpdateModule(Module module)
        {
            
        }

        public void Validate(MessageBase message)
        {
            //
        }
    }
}
