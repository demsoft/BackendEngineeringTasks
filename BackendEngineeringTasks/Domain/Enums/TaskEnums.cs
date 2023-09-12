namespace BackendEngineeringTasks.Domain.Enums
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Pending,
        InProgress,
        Completed
    }

    public enum NotificationType
    {
        DueDateReminder,   // Notification for upcoming due dates
        StatusUpdate,      // Notification for task status updates
        Assignment         // Notification for task assignments
                           // Add more types as needed
    }
}
