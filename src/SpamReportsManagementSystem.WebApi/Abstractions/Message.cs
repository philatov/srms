using System;

namespace SpamReportsManagementSystem
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public User Author { get; set; }
        public DateTime Timestamp { get; internal set; }
        public int ReportsCount { get; internal set; }
    }
}