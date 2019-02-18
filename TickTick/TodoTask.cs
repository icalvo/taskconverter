using System;

namespace TickTick
{
    public class TodoTask
    {
        public string FolderName { get; set; }
        public string ListName { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Content { get; set; }
        public bool IsCheckList { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public TimeSpan? Reminder { get; set; }
        public Repeat Repeat { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public long Order { get; set; }
        public string TimeZone { get; set; }
        public bool IsAllDay { get; set; }
    }
}