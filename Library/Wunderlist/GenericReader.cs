using Library.Generic;
using Library.TickTick;

namespace Library.Wunderlist
{
    public class GenericReader : BaseTaskDatabaseReader<Backup>
    {
        public GenericReader()
        {
            ModelReader = new Reader();
        }
        protected override IReader<Backup> ModelReader { get; }
        public override TaskDatabase Convert(Backup source)
        {
            return source.ToGeneric();
        }

        public override string Name => "wunderlist";
    }
}