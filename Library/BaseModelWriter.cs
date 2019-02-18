using System.IO;
using Library.Generic;

namespace Library
{
    public abstract class BaseModelWriter<T> : IWriter<TaskDatabase>
    {

        protected abstract IWriter<T> ModelWriter { get; }
        public abstract T Convert(TaskDatabase db);

        public void Write(TaskDatabase value, Stream stream)
        {
            var model = Convert(value);
            ModelWriter.Write(model, stream);
        }
    }
}