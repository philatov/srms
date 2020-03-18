using System;

namespace SpamReportsManagementSystem
{
    public class Report
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public User From { get; set; }

        public User About { get; set; }

        public Message Message { get; set; }

        public Manager Manager { get; set; }

        public ReportStatus ReportStatus { get; set; }

    }
}
