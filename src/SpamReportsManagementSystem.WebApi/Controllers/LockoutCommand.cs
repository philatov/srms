using System;

namespace SpamReportsManagementSystem.Controllers
{
    public class LockoutCommand
    {
        public string LockComment { get; set; }
        public Guid ManagerId { get; set; }
        public Guid ReportId { get; set; }
        public DateTimeOffset Till { get; set; }
    }
}