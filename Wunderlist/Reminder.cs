using System;

namespace Wunderlist
{
    public class Reminder
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long TaskId { get; set; }
    }
}