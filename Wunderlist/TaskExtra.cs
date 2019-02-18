using System.Collections.Generic;

namespace Wunderlist
{
    public class TaskExtra
    {
        public TaskExtra(TaskList list,
            IEnumerable<Subtask> subtasks,
            Reminder reminder,
            Note note,
            SubtaskPosition subtaskPositions)
        {
            List = list;
            Subtasks = subtasks;
            //if (subtasks != null)
            //{
            //    Subtasks =
            //        subtaskPositions.Values
            //            .Select(subtaskId => subtasks.SingleOrDefault(x => x.Id == subtaskId))
            //            .Where(x => x != null)
            //            .ToList();
            //    Debug.Assert(subtasks.Count() == Subtasks.Count());
            //}

            Reminder = reminder;
            Note = note;
        }

        public TaskList List { get; }
        public IEnumerable<Subtask> Subtasks { get; }
        public Reminder Reminder { get; }
        public Note Note { get; }
    }
}