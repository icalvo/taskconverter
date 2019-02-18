using Library.Generic;

namespace Library
{
    public interface ITaskDatabaseWriter : IWriter<TaskDatabase>
    {
        string Name { get; }
    }
}