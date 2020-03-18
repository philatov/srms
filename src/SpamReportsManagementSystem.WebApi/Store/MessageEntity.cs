using System;

namespace SpamReportsManagementSystem.Store
{
    public class MessageEntity
    {
        public Guid Uid { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User { get; set; }

        public string Text { get; set; }
    }
}