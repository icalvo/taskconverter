using System;

namespace Library.Wunderlist
{
    public class TodoTask
    {
        public long Id { get; set; }
        public RecurrenceType? RecurrenceType { get; set; }
        public int RecurrenceCount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool Starred { get; set; }
        public long ListId { get; set; }
        public string Title { get; set; }
    }
}