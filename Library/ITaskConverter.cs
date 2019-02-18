using Library.Generic;

namespace Library
{
    public interface ITaskConverter
    {
        IReader<TaskDatabase> Reader { get; }
        IWriter<TaskDatabase> Writer { get; }
    }
}