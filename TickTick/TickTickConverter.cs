using System;
using System.Collections.Generic;
using System.Linq;
using Library;
using Library.Generic;

namespace TickTick
{
    public class TickTickConverter : BaseModelWriter<IEnumerable<TodoTask>>, ITaskConverter
    {
        public TickTickConverter()
        {
            ModelWriter = new Writer();
        }

        protected override IWriter<IEnumerable<TodoTask>> ModelWriter { get; }

        public override IEnumerable<TodoTask> Convert(TaskDatabase db)
        {
            return db.Tasks.Select(Convert).ToList();
        }

        public IReader<TaskDatabase> Reader => throw new NotImplementedException();
        public IWriter<TaskDatabase> Writer => this;

        private static TodoTask Convert(
            Library.Generic.TodoTask task)
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

        private static Priority ConvertPriority(Library.Generic.TodoTask task)
        {
            switch (task.Starred.Total)
            {
                case 1:
                    return Priority.None;
                case 2:
                    return task.Starred.Level == 0? Priority.None : Priority.High;
                default:
                    throw new ArgumentOutOfRangeException(nameof(task), "Cannot convert priorities with " + task.Starred.Level +
                                                  " levels to TickTick priorities.");
            }
        }

        private static TimeSpan? ConvertReminder(Library.Generic.TodoTask task, Library.Generic.Reminder reminder)
        {
            return
                task.DueDate.HasValue
                    ? reminder?.Date.Subtract(task.DueDate.Value)
                    : null;
        }

        private static Repeat ConvertRepeat(Library.Generic.TodoTask task)
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

        private static RepeatType Convert(Library.Generic.RecurrenceType recurrenceType)
        {
            switch (recurrenceType)
            {
                case Library.Generic.RecurrenceType.Year:  return RepeatType.Yearly;
                case Library.Generic.RecurrenceType.Month: return RepeatType.Monthly;
                case Library.Generic.RecurrenceType.Week:  return RepeatType.Weekly;
                case Library.Generic.RecurrenceType.Day:   return RepeatType.Daily;
                default:
                    throw new ArgumentOutOfRangeException(nameof(recurrenceType), recurrenceType, null);
            }
        }

        private static string Content(Library.Generic.TodoTask task)
        {
            return task.Note?.Content + string.Join("\r\n",
                       task.Subtasks?.Select(x => x.Completed ? "▪" + x.Title : "▫" + x.Title) ?? new string[0]);
        }
    }
}