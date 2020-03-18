using System;

namespace SpamReportsManagementSystem
{
    public class Warning
    {
        public Guid Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public User User { get; set; }

        public Manager Manager { get; set; }

        public Report Report { get; set; }

        public string Comment { get; set; }
    }
}