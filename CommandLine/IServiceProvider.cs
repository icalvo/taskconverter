using System;
using System.Collections.Generic;

namespace CommandLine
{
    public interface IServiceProvider
    {
        T GetInstanceByTypeName<T>(string typeName);
    }
}