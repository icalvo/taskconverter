using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using Library;

namespace TickTick
{
    public class Writer: IWriter<IEnumerable<TodoTask>>

    {
    public void Write(IEnumerable<TodoTask> tickTickTasks, Stream output)
    {
        using (var writer = new StreamWriter(output, Encoding.UTF8))
        using (var csv = new CsvWriter(writer))
        {
            csv.Configuration.RegisterClassMap<CsvTodoTaskMap>();
            csv.Configuration.ShouldQuote = (_, __) => true;
            csv.Configuration.Delimiter = ",";
            writer.WriteLine("\"Date: 2019-02-13+0000\"");
            writer.WriteLine("\"Version: 5.0\"");
            writer.WriteLine(@"""Status: 
0 Normal
1 Completed
2 Archived""");
            csv.WriteRecords(tickTickTasks);
        }
    }
    }
}