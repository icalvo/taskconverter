using System.Collections.Generic;
using System.Linq;

namespace Library.Generic
{
    public class TaskDatabase
    {
        public IEnumerable<TodoTask> Tasks { get; set; }
        public IEnumerable<TaskList> Lists { get; set; }
    }
}
