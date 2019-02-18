using System.Collections.Generic;

namespace Library.Wunderlist
{
    public class BackupData
    {
        public IEnumerable<TaskList> Lists { get; set; }
        public IEnumerable<TodoTask> Tasks { get; set; }
        public IEnumerable<Reminder> Reminders { get; set; }
        public IEnumerable<Subtask> Subtasks { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<TaskPosition> TaskPositions { get; set; }
        public IEnumerable<SubtaskPosition> SubtaskPositions { get; set; }
    }
}