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
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly ReportsDbContext _dbContext;

        public ReportsController(ILogger<ReportsController> logger, ReportsDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Report>> GetAsync()
        {
            var query = from r in _dbContext.Set<ReportEntity>()
                        join ua in _dbContext.Set<UserEntity>() on r.AboutId equals ua.Uid
                        join uf in _dbContext.Set<UserEntity>() on r.FromId equals uf.Uid                        
                        join ms in _dbContext.Set<MessageEntity>() on r.MessageId equals ms.Uid
                        join ma in _dbContext.Set<UserEntity>() on ms.UserId equals ma.Uid
                        join tmn in _dbContext.Set<ManagerEntity>() on r.ManagerId equals tmn.Uid into mnList
                        from mn in mnList.DefaultIfEmpty()
                        orderby r.Timestamp 
                        select new Report
                        {
                            Id = r.Uid,
                            About = new User 
                            { 
                                Id = ua.Uid,
                                Name = ua.Name
                            },
                            Timestamp = r.Timestamp.UtcDateTime,
                            From = new User
                            {
                                Id = uf.Uid,
                                Name = uf.Name
                            },
                            Manager = new Manager 
                            { 
                                Id = mn.Uid,
                                Name = mn.Name
                            },
                            Message = new Message 
                            { 
                                Id = ms.Uid,
                                Text = ms.Text,
                                Author = new User
                                {
                                    Id = ma.Uid,
                                    Name = ma.Name
                                }
                            },
                            ReportStatus = (ReportStatus)r.StatusId
                        };

            return await query.ToArrayAsync();
        }
    }
}
