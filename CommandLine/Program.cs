using System;
using System.IO;
using System.Linq;
using CsvHelper;
using Library;
using Library.Generic;

namespace CommandLine
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            GenericConverter converter = GetConverter(args[0], args[1],
                args.Skip(4).ToArray());
            if (converter == null)
            {
                Console.Error.WriteLine("Unsupported conversion!");
                return;
            }

            string sourceFileName = args[2];
            string targetFileName = args[3];
            converter.Convert(
                File.OpenRead(sourceFileName),
                File.OpenWrite(targetFileName));
        }

        private static GenericConverter GetConverter(string source,
            string target, string[] options)
        {
            IReader<TaskDatabase> reader = GetReader(source.ToLowerInvariant());
            IWriter<TaskDatabase> writer = GetWriter(target.ToLowerInvariant());
            var opts = new ConversionOptions
            {
                ListConversionMode = options.Contains("-listsastags")
                    ? ListConversionMode.ListsAsTags
                    : ListConversionMode.ListsAsLists
            };
            return new GenericConverter(reader, writer, opts);
        }

        private static IReader<TaskDatabase> GetReader(string source)
        {
            var type = typeof(ITaskDatabaseReader);
            return 
                AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.FullName.StartsWith("System."))
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract)
                .Where(p => p.HasParameterlessConstructor())
                .Select(Activator.CreateInstance)
                .Cast<ITaskDatabaseReader>()
                .First(o => o.Name == source);
        }

        private static IWriter<TaskDatabase> GetWriter(string target)
        {
            var type = typeof(ITaskDatabaseWriter);
            return
                AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.FullName.StartsWith("System."))
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsAbstract)
                .Where(p => p.HasParameterlessConstructor())
                .Select(Activator.CreateInstance)
                .Cast<ITaskDatabaseWriter>()
                .First(o => o.Name == target);
        }
    }
}
