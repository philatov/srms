using System;

namespace SpamReportsManagementSystem.Store
{
    public class ReportEntity
    {
        public Guid Uid { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public Guid FromId { get; set; }

        public UserEntity From { get; set; }

        public Guid AboutId { get; set; }

        public UserEntity About { get; set; }

        public Guid ManagerId { get; set; }

        public ManagerEntity Manager { get; set; }

        public Guid MessageId { get; set; }

        public MessageEntity Message { get; set; }

        public int StatusId { get; set; }
    }
}
