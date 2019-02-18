using System.IO;
using Library.Generic;

namespace Library
{
    public abstract class BaseModelReader<T> : IReader<TaskDatabase>
    {
        protected abstract IReader<T> ModelReader { get; }
        public abstract TaskDatabase Convert(T source);

        public TaskDatabase Read(Stream stream)
        {
            var model = ModelReader.Read(stream);
            return Convert(model);
        }
    }
}