namespace digitalsign.domain.Domain
{
    public class DailyReminder : Reminder
    {
        public DailyReminder() : base(common.Enumeration.ReminderType.Daily) { }
    }
}
