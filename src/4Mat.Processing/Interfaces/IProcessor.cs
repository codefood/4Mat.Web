using _4Mat.Data;
using _4Mat.Processing.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Processing.Interfaces
{
    public interface IProcessor<T> where T : MessageBase
    {
        void ProcessMessage(T message);

        IMessageValidator MessageValidator { get; }
        IModuleService ModuleService { get; }

    }
}
