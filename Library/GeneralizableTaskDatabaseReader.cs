using Library.Generic;
using Library.Wunderlist;

namespace Library
{
    public abstract class GeneralizableTaskDatabaseReader<T> : BaseTaskDatabaseReader<T>
        where T: IGeneralizable
    {
        public override TaskDatabase Convert(T source)
        {
            return source.ToGeneric();
        }
    }
}