using Library.Generic;

namespace Library
{
    public abstract class BaseTaskConverter : ITaskConverter
    {
        public abstract IReader<TaskDatabase> Reader { get; }
        public abstract IWriter<TaskDatabase> Writer { get; }
    }
}