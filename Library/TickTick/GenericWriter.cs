using System;
using System.Collections.Generic;
using System.Linq;
using Library.Generic;

namespace Library.TickTick
{
    public class GenericWriter : BaseTaskDatabaseWriter<IEnumerable<TodoTask>>
    {
        public GenericWriter()
        {
            ModelWriter = new Writer();
        }

        protected override IWriter<IEnumerable<TodoTask>> ModelWriter { get; }

        public override IEnumerable<TodoTask> Convert(TaskDatabase db)
        {
            return db.Tasks.Select(Convert).ToList();
        }

        public override string Name => "ticktick";

        private static TodoTask Convert(
            Generic.TodoTask task)
        {
            return new TodoTask
            {
                FolderName = "",
                ListName = task.List.Title,
                Title = task.Title,
                Tags = string.Join(", ", task.Tags),
                Content = Content(task),
                IsCheckList = task.Subtasks.Any(),
                StartDate = null,
                DueDate = task.DueDate,
                Reminder = ConvertReminder(task, task.Reminder),
                Repeat = ConvertRepeat(task),
                Priority = ConvertPriority(task),
                Status = task.Completed ? Status.Completed : Status.Pending,
                CreatedTime = null,
                CompletedTime = task.CompletedAt,
                Order = 0,
                TimeZone = "UTC",
                IsAllDay = true,
            };
        }

        private static Priority ConvertPriority(Generic.TodoTask task)
        {
            switch (task.Starred.Total)
            {
                case 1:
                    return Priority.None;
                case 2:
                    return task.Starred.Level == 0? Priority.None : Priority.High;
                default:
                    throw new NotImplementedException("Cannot convert priorities with " + task.Starred.Level +
                                                  " levels to TickTick priorities.");
            }
        }

        private static TimeSpan? ConvertReminder(Generic.TodoTask task, Generic.Reminder reminder)
        {
            return
                task.DueDate.HasValue
                    ? reminder?.Date.Subtract(task.DueDate.Value)
                    : null;
        }

        private static Repeat ConvertRepeat(Generic.TodoTask task)
        {
            if (task.RecurrenceType == null)
            {
                return null;
            }

            return new Repeat
            {
                RepeatCount = task.RecurrenceCount,
                RepeatType = Convert(task.RecurrenceType.Value)
            };
        }

        private static RepeatType Convert(Generic.RecurrenceType recurrenceType)
        {
            switch (recurrenceType)
            {
                case Generic.RecurrenceType.Year:  return RepeatType.Yearly;
                case Generic.RecurrenceType.Month: return RepeatType.Monthly;
                case Generic.RecurrenceType.Week:  return RepeatType.Weekly;
                case Generic.RecurrenceType.Day:   return RepeatType.Daily;
                default:
                    throw new ArgumentOutOfRangeException(nameof(recurrenceType), recurrenceType, null);
            }
        }

        private static string Content(Generic.TodoTask task)
        {
            return task.Note?.Content + string.Join("\r\n",
                       task.Subtasks?.Select(x => x.Completed ? "▪" + x.Title : "▫" + x.Title) ?? new string[0]);
        }
    }
}