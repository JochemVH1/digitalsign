using System;

namespace digitalsign.domain.Domain
{
    public class WeeklyReminder : Reminder
    {
        public DayOfWeek DayOfWeek { get; set; }

        public WeeklyReminder() : base(common.Enumeration.ReminderType.Yearly) { }
    }
}
