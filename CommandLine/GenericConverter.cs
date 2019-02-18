using System.IO;
using Library;
using Library.Generic;

namespace CommandLine
{
    public class GenericConverter
    {
        private readonly IReader<TaskDatabase> _reader;
        private readonly IWriter<TaskDatabase> _writer;
        private readonly ConversionOptions _options;

        public GenericConverter(
            IReader<TaskDatabase> reader,
            IWriter<TaskDatabase> writer,
            ConversionOptions options = null)
        {
            _reader = reader;
            _writer = writer;
            _options = options;
        }

        public void Convert(Stream input, Stream output, string inputName)
        {
            TaskDatabase source = _reader.Read(input);
            Transform(inputName, source);
            _writer.Write(source, output);
        }

        private void Transform(string inputName, TaskDatabase source)
        {
            if (_options.ListConversionMode == ListConversionMode.ListsAsTags)
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