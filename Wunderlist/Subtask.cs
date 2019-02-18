namespace Wunderlist
{
    public class Subtask
    {
        public long Id { get; set; }
        public bool Completed { get; set; }
        public long TaskId { get; set; }
        public string Title { get; set; }
    }
}