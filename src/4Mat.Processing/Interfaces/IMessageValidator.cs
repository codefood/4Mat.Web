using _4Mat.Data;
using _4Mat.Data.External;
using _4Mat.Processing.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Processing.Interfaces
{
    public interface IMessageValidator
    {
        void Validate(MessageBase message);
        void UpdateModule(Module module);
        IList<Rule> ValidationRules { get; }
    }
}
