using System;
using System.Collections.Generic;
using System.Linq;
using Library.Generic;
using Wunderlist.Utils;

namespace Wunderlist
{
    public class Backup
    {
        public long User { get; set; }
        public string Exported { get; set; }
        public BackupData Data { get; set; }

        public Library.Generic.TaskDatabase ToGeneric()
        {
            return new Library.Generic.TaskDatabase
            {
                Lists = Data.Lists.Select(x =>
                    new Library.Generic.TaskList
                    {
                        Id = x.Id,
                        Title = x.Title
                    }).ToList(),
                Tasks = BuildExtras().Select(tuple =>
                {
                    var (task, extra) = tuple;
                    return new Library.Generic.TodoTask
                    {
                        Completed = task.Completed,
                        CompletedAt = task.CompletedAt,
                        DueDate = task.DueDate,
                        Id = task.Id,
                        RecurrenceType = Convert(task.RecurrenceType),
                        RecurrenceCount = task.RecurrenceCount,
                        Starred = task.Starred? new Priority
                        {
                            Level = 1,
                            Title = "Not starred",
                            Total = 2
                        } : new Priority
                        {
                            Level = 0,
                            Title = "Starred",
                            Total = 2
                        },
                        Title = task.Title,
                        Tags = new []{ "wunderlist" },
                        List = new Library.Generic.TaskList
                        {
                            Id = extra.List.Id,
                            Title = extra.List.Title
                        },
                        Note = extra.Note == null? null:
                            new Library.Generic.Note
                            {
                                Id = extra.Note.Id,
                                Content = extra.Note.Content
                            },
                        Subtasks = extra.Subtasks?.Select(x => new Library.Generic.Subtask
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Completed = x.Completed
                        }) ?? new Library.Generic.Subtask[0],
                        Reminder = extra.Reminder == null? null:
                            new Library.Generic.Reminder
                            {
                                Id = extra.Reminder.Id,
                                Date = extra.Reminder.Date
                            }
                    };
                }).ToList()
            };
        }

        private Library.Generic.RecurrenceType? Convert(RecurrenceType? taskRecurrenceType)
        {
            if (!taskRecurrenceType.HasValue)
            {
                return null;
            }

            switch (taskRecurrenceType.Value)
            {
                case RecurrenceType.Year:
                    return Library.Generic.RecurrenceType.Year;
                case RecurrenceType.Month:
                    return Library.Generic.RecurrenceType.Month;
                case RecurrenceType.Week:
                    return Library.Generic.RecurrenceType.Week;
                case RecurrenceType.Day:
                    return Library.Generic.RecurrenceType.Day;
                default:
                    throw new ArgumentOutOfRangeException(nameof(taskRecurrenceType));
            }
        }

        public IEnumerable<(TodoTask, TaskExtra)> BuildExtras()
        {
            var dictionary = new Dictionaries(this);
            return Data.Tasks.Select(x => (x, dictionary.GetExtra(x))).ToList();
        }

        private class Dictionaries
        {
            public Dictionaries(Backup wunderlistBackup)
            {
                Lists = wunderlistBackup.Data.Lists.ToDictionary(x => x.Id);
                Subtasks = wunderlistBackup.Data.Subtasks.GroupBy(x => x.TaskId)
                    .ToDictionary(x => x.Key, x => (IEnumerable<Subtask>) x);
                Reminders = wunderlistBackup.Data.Reminders.GroupBy(x => x.TaskId).ToDictionary(x => x.Key, x => x.First());
                Notes = wunderlistBackup.Data.Notes.ToDictionary(x => x.TaskId);
                SubtaskPositions = wunderlistBackup.Data.SubtaskPositions.ToDictionary(x => x.TaskId);
            }

            public Dictionary<long, SubtaskPosition> SubtaskPositions { get; set; }

            public Dictionary<long, TaskList> Lists { get; }
            public Dictionary<long, IEnumerable<Subtask>> Subtasks { get; }
            public Dictionary<long, Wunderlist.Reminder> Reminders { get; }
            public Dictionary<long, Note> Notes { get; }

            public TaskExtra GetExtra(Wunderlist.TodoTask task)
            {
                return new TaskExtra(
                    Lists[task.ListId],
                    Subtasks.TryGet(task.Id),
                    Reminders.TryGet(task.Id),
                    Notes.TryGet(task.Id),
                    SubtaskPositions.TryGet(task.Id));
            }
        }
    }
}
