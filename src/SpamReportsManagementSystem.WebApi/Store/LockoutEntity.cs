using System;

namespace SpamReportsManagementSystem.Store
{
    public class LockoutEntity
    {
        public Guid Uid { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        public Guid ManagerId { get; set; }

        public ManagerEntity Manager { get; set; }

        public Guid? ReportId { get; set; }

        public ReportEntity Report { get; set; }

        public DateTimeOffset Till { get; set; }

        public string Comment { get; set; }
    }
}