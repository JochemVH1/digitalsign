namespace digitalsign.domain.Domain
{
    public class YearlyReminder : Reminder
    {
        public int Day { get; set; }

        public int Month { get; set; }

        public YearlyReminder() : base(common.Enumeration.ReminderType.Yearly) { }
    }
}
