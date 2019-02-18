using Library.Generic;

namespace Library
{
    public interface ITaskDatabaseReader : IReader<TaskDatabase>
    {
        string Name { get; }
    }
}