namespace digitalsign.domain.Domain
{
    public class MonthlyReminder : Reminder
    {
        public int DayOfTheMonth { get; set; }
        public MonthlyReminder() : base(common.Enumeration.ReminderType.Monthly) { }
    }
}
