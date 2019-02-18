using System;
using CsvHelper.Configuration;

namespace TickTick
{
    public static class MapExtensions
    {
        public static MemberMap<TClass, TMember> ExportWith<TClass, TMember>(this MemberMap<TClass, TMember> m,
            Func<TMember, object> convert)
        {
            return m.TypeConverter(new ExportConverter<TMember>(convert));
        }
    }
}