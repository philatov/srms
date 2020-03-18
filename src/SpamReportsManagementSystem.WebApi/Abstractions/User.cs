using System;

namespace SpamReportsManagementSystem
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MessagesCount { get; set; }
        public int ReportsCount { get; set; }
        public int WarningsCount { get; set; }
        public DateTime? LockedOutTill { get; set; }
        public bool IsLockedOut { get; set; }
        public string LockComment { get; set; }
        public string WarnComment { get; set; }
    }
}