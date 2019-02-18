using System.IO;
using Library.Generic;

namespace Library
{
    public abstract class BaseTaskDatabaseReader<T> : ITaskDatabaseReader
    {
        protected abstract IReader<T> ModelReader { get; }
        public abstract TaskDatabase Convert(T source);
        public abstract string Name { get; }

        public TaskDatabase Read(Stream stream)
        {
            var model = ModelReader.Read(stream);
            return Convert(model);
        }
    }
}