using System;

namespace SpamReportsManagementSystem.Controllers
{
    public class WarnCommand
    {
        public Guid UserId { get; set; }
        public string WarnComment { get; set; }
        public Guid ManagerId { get; set; }
        public Guid ReportId { get; set; }
    }
}