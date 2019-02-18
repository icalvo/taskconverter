using System;
using Library;
using Library.Generic;

namespace Wunderlist
{
    public class WunderlistConverter : BaseModelReader<Backup>, ITaskConverter
    {
        public WunderlistConverter()
        {
            ModelReader = new Reader();
        }

        protected override IReader<Backup> ModelReader { get; }

        public override TaskDatabase Convert(Backup source)
        {
            return source.ToGeneric();
        }

        public IReader<TaskDatabase> Reader => this;
        public IWriter<TaskDatabase> Writer => throw new NotImplementedException();
    }
}