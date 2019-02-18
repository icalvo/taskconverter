using System;
using System.IO;
using System.Linq;
using Library;
using Library.Generic;

namespace CommandLine
{
    internal static class Program
    {
        private static IServiceProvider _serviceProvider = new ServiceProvider();

        internal static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                throw new ArgumentException("At least 4 parameters are needed (source, target, source file, target file", nameof(args));
            }

            _serviceProvider = new ServiceProvider();
            string source = args[0];
            string target = args[1];
            var options = args.Skip(4).ToArray();
            var sourceModelConverter = _serviceProvider.GetInstanceByTypeName<ITaskConverter>(source + "Converter");
            var targetModelConverter = _serviceProvider.GetInstanceByTypeName<ITaskConverter>(target + "Converter");
            IReader<TaskDatabase> reader = sourceModelConverter.Reader ?? throw new ArgumentException("Cannot read tasks from " + source, nameof(source));
            IWriter<TaskDatabase> writer = targetModelConverter.Writer ?? throw new ArgumentException("Cannot write tasks from " + source, nameof(source));
            var opts = new ConversionOptions
            {
                ListConversionMode = options.Contains("-listsastags")
                    ? ListConversionMode.ListsAsTags
                    : ListConversionMode.ListsAsLists
            };
            string sourceFileName = args[2];
            string targetFileName = args[3];
            var sourceStream = File.OpenRead(sourceFileName);
            var targetStream = File.OpenWrite(targetFileName);

            TaskDatabase db = reader.Read(sourceStream);
            Transform(source, db, opts);
            writer.Write(db, targetStream);
        }

        private static void Transform(string inputName, TaskDatabase source, ConversionOptions options)
        {
            if (options.ListConversionMode == ListConversionMode.ListsAsTags)
            {
                var uniqueList = new TaskList {Id = 0, Title = inputName + " import"};
                source.Lists = new[] {uniqueList};
                foreach (var task in source.Tasks)
                {
                    task.Tags = new[] {task.List.Title};
                    task.List = uniqueList;
                }
            }
        }
    }
}
