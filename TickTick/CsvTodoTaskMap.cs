using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CsvHelper.Configuration;

namespace TickTick
{
    public class CsvTodoTaskMap : ClassMap<TodoTask>
    {
        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public CsvTodoTaskMap()
        {
            string FormatDate(DateTime? x)
            {
                return x == null ? "": x.Value.ToString("s") + "+0000";
            }

            Map(m => m.FolderName   ).Index( 0).Name(Pascal2Title(nameof(TodoTask.FolderName   )));
            Map(m => m.ListName     ).Index( 1).Name(Pascal2Title(nameof(TodoTask.ListName     )));
            Map(m => m.Title        ).Index( 2).Name(Pascal2Title(nameof(TodoTask.Title        )));
            Map(m => m.Tags         ).Index( 3).Name(Pascal2Title(nameof(TodoTask.Tags         )));
            Map(m => m.Content      ).Index( 4).Name(Pascal2Title(nameof(TodoTask.Content      )));
            Map(m => m.IsCheckList  ).Index( 5).Name(Pascal2Title(nameof(TodoTask.IsCheckList  )))
                .ExportWith(x => x? "Y": "N");
            Map(m => m.StartDate    ).Index( 6).Name(Pascal2Title(nameof(TodoTask.StartDate    )))
                .ExportWith(FormatDate);
            Map(m => m.DueDate      ).Index( 7).Name(Pascal2Title(nameof(TodoTask.DueDate      )))
                .ExportWith(FormatDate);
            Map(m => m.Reminder     ).Index( 8).Name(Pascal2Title(nameof(TodoTask.Reminder     )))
                .ExportWith(x => x == null? "" : ConvertReminder(x.Value));
            Map(m => m.Repeat       ).Index( 9).Name(Pascal2Title(nameof(TodoTask.Repeat       )))
                .ExportWith(x => x == null ? "": "FREQ=" + x.RepeatType.ToString().ToUpperInvariant() + ";INTERVAL=" + x.RepeatCount);
            Map(m => m.Priority     ).Index(10).Name(Pascal2Title(nameof(TodoTask.Priority     )))
                .ExportWith(x => (int)x);
            Map(m => m.Status       ).Index(11).Name(Pascal2Title(nameof(TodoTask.Status       )))
                .ExportWith(x => (int)x);
            Map(m => m.CreatedTime  ).Index(12).Name(Pascal2Title(nameof(TodoTask.CreatedTime  )))
                .ExportWith(FormatDate);
            Map(m => m.CompletedTime).Index(13).Name(Pascal2Title(nameof(TodoTask.CompletedTime)))
                .ExportWith(FormatDate);
            Map(m => m.Order        ).Index(14).Name(Pascal2Title(nameof(TodoTask.Order        )));
            Map(m => m.TimeZone     ).Index(15).Name(Pascal2Title(nameof(TodoTask.TimeZone     )));
            Map(m => m.IsAllDay     ).Index(16).Name(Pascal2Title(nameof(TodoTask.IsAllDay     )))
                .ExportWith(x => x? "true": "false");
        }

        private static string ConvertReminder(TimeSpan argValue)
        {
            if (argValue == TimeSpan.Zero)
            {
                return "PT0S\r\n";
            }

            string format = (argValue < TimeSpan.Zero? @"\-" : "") + @"\Pd\D\Th\Hm\Ms\S";
            try
            {
                return argValue.ToString(format) + "\r\n";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static string Pascal2Title(string input)
        {
            return Regex.Replace(input, @"\p{Lu}", m => " " + m.Value.ToUpperInvariant()).Substring(1);
        }
    }
}