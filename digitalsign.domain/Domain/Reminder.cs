using digitalsign.common.Enumeration;

namespace digitalsign.domain.Domain
{
    public abstract class Reminder : Message
    {

        protected Reminder(ReminderType reminderType)
        {
            ReminderType = reminderType;
        }

        protected ReminderType ReminderType { get; set; }
    }
}
