using System.Collections.Generic;

namespace Library.Generic
{
    public class TaskPosition
    {
        public long Id { get; set; }
        public long ListId { get; set; }
        public IEnumerable<long> Values { get; set; }
    }
}