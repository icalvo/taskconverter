using System.Collections.Generic;

namespace Library.Wunderlist
{
    public class SubtaskPosition
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public IEnumerable<long> Values { get; set; }
    }
}