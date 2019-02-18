using System;
using System.Collections.Generic;
using System.Reflection;

namespace Library.Generic
{
    public class TodoTask
    {
        public long Id { get; set; }
        public RecurrenceType? RecurrenceType { get; set; }
        public int RecurrenceCount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Priority Starred { get; set; }
        public string Title { get; set; }

        public TaskList List { get; set; }
        public IEnumerable<Subtask> Subtasks { get; set; }
        public Reminder Reminder { get; set; }
        public Note Note { get; set; }
        public string[] Tags { get; set; }
    }

    public class Priority
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public int Total { get; set; }
    }
}