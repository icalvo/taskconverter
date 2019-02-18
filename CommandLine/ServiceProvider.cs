using System;
using System.Linq;

namespace CommandLine
{
    internal class ServiceProvider : IServiceProvider
    {
        public T GetInstanceByTypeName<T>(string typeName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !assembly.FullName.StartsWith("System."))
                    .SelectMany(s => s.GetTypes())
                    .Where(p =>
                        typeof(T).IsAssignableFrom(p)
                        && p.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase)
                        && !p.IsAbstract
                        && p.GetConstructor(Type.EmptyTypes) != null)
                    .Select(Activator.CreateInstance)
                    .Cast<T>()
                    .First();
        }
    }
}