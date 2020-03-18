using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpamReportsManagementSystem.Store;

namespace SpamReportsManagementSystem.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly ReportsDbContext _dbContext;

        public MessagesController(ILogger<MessagesController> logger, ReportsDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Message>> GetAsync()
        {
            var query = from ms in _dbContext.Set<MessageEntity>()
                        join a in _dbContext.Set<UserEntity>() on ms.UserId equals a.Uid
                        select new Message
                        {
                            Id = ms.Uid,
                            Timestamp = ms.Timestamp.UtcDateTime,
                            Text = ms.Text,
                            Author = new User
                            {
                                Id = a.Uid,
                                Name = a.Name
                            },
                            ReportsCount = _dbContext.Set<ReportEntity>().Count(r => r.MessageId == a.Uid)
                        };

            return await query.ToArrayAsync();
        }
    }
}
