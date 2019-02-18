using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TickTick
{
    public class ExportConverter<T> : DefaultTypeConverter
    {
        private readonly Func<T, object> _convert;

        public ExportConverter(Func<T, object> convert)
        {
            _convert = convert;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return _convert((T) value)?.ToString();
        }
    }
}