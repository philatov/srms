using System;
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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ReportsDbContext _dbContext;

        public UsersController(ILogger<UsersController> logger, ReportsDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            var query = from u in _dbContext.Set<UserEntity>()
                        select new User
                        {
                            Id = u.Uid,
                            Name = u.Name,
                            MessagesCount = _dbContext.Set<MessageEntity>().Count(m => m.UserId == u.Uid),
                            ReportsCount = _dbContext.Set<ReportEntity>().Count(r => r.AboutId == u.Uid),
                            WarningsCount = _dbContext.Set<WarningEntity>().Count(w => w.UserId == u.Uid),
                            IsLockedOut = _dbContext.Set<LockoutEntity>().Any(l => l.UserId == u.Uid && l.Till > DateTime.UtcNow),
                            LockedOutTill = _dbContext.Set<LockoutEntity>().Where(l => l.UserId == u.Uid && l.Till > DateTime.UtcNow).Max(l => l.Till).UtcDateTime,
                            LockComment = _dbContext.Set<LockoutEntity>().Where(l => l.UserId == u.Uid && l.Till > DateTime.UtcNow).OrderByDescending(l => l.Till).FirstOrDefault().Comment,
                            WarnComment = _dbContext.Set<WarningEntity>().Where(l => l.UserId == u.Uid).OrderByDescending(w => w.Timestamp).FirstOrDefault().Comment,
                        };

            return await query.ToArrayAsync();
        }

        [HttpPost("{id:guid}/warn")]
        public async Task<IActionResult> WarnAsync([FromRoute]Guid id, [FromBody]WarnCommand command) 
        {
            var query = from m in _dbContext.Set<ManagerEntity>()
                        select m.Uid;

            var warning = new WarningEntity() 
            {
                Uid = Guid.NewGuid(),
                UserId = id,
                Timestamp = DateTime.UtcNow,
                Comment = command.WarnComment,
                ManagerId = command.ManagerId == Guid.Empty ? query.FirstOrDefault() : command.ManagerId,
                ReportId = command.ReportId == Guid.Empty ? (Guid?)null : command.ReportId,
            };

            _dbContext.Set<WarningEntity>().Add(warning);

            await _dbContext.SaveChangesAsync();

            var user = await GetUserAsync(id);

            return Ok(user);
        }

        [HttpPost("{id:guid}/lock")]
        public async Task<IActionResult> LockAsync([FromRoute]Guid id, [FromBody]LockoutCommand command) 
        {
            var query = from m in _dbContext.Set<ManagerEntity>()
                        select m.Uid;
                       
            var lockout = new LockoutEntity()
            {
                Uid = Guid.NewGuid(),
                UserId = id,
                Timestamp = DateTime.UtcNow,
                Comment = command.LockComment,
                ManagerId = command.ManagerId == Guid.Empty ? query.FirstOrDefault() : command.ManagerId,
                ReportId = command.ReportId == Guid.Empty ? (Guid?)null : command.ReportId, 
                Till = command.Till == default ? DateTimeOffset.UtcNow.AddDays(1) : command.Till
            };

            _dbContext.Set<LockoutEntity>().Add(lockout);

            await _dbContext.SaveChangesAsync();

            var user = await GetUserAsync(id);

            return Ok(user);
        }

        [HttpPost("{id:guid}/unlock")]
        public async Task<IActionResult> UnlockAsync([FromRoute]Guid id)
        {
            var lockoutIdsQuery = from l in _dbContext.Set<LockoutEntity>()
                                  where l.UserId == id && l.Till > DateTime.UtcNow
                                  select l.Uid;

            var lockoutIds = await lockoutIdsQuery.ToArrayAsync();

            var now = DateTimeOffset.UtcNow;

            foreach (var l in lockoutIds)
            {
                var lockout = new LockoutEntity()
                {
                    Uid = l
                };

                var entry = _dbContext.Attach(lockout);

                var property = entry.Property(nameof(LockoutEntity.Till));
                property.CurrentValue = now;
                property.IsModified = true;
            }

            await _dbContext.SaveChangesAsync();

            var user = await GetUserAsync(id);

            return Ok(user);
        }

        private async Task<User> GetUserAsync(Guid id)
        {
            var userQuery = from u in _dbContext.Set<UserEntity>()
                            where u.Uid == id
                            select new User
                            {
                                Id = u.Uid,
                                Name = u.Name,
                                MessagesCount = _dbContext.Set<MessageEntity>().Count(m => m.UserId == u.Uid),
                                ReportsCount = _dbContext.Set<ReportEntity>().Count(r => r.AboutId == u.Uid),
                                WarningsCount = _dbContext.Set<WarningEntity>().Count(w => w.UserId == u.Uid),
                                IsLockedOut = _dbContext.Set<LockoutEntity>().Any(l => l.UserId == u.Uid && l.Till > DateTime.UtcNow),
                                LockedOutTill = _dbContext.Set<LockoutEntity>().Where(l => l.UserId == u.Uid && l.Till > DateTime.UtcNow).Max(l => l.Till).UtcDateTime,
                                LockComment = _dbContext.Set<LockoutEntity>().Where(l => l.UserId == u.Uid && l.Till > DateTime.UtcNow).OrderByDescending(l => l.Till).FirstOrDefault().Comment,
                                WarnComment = _dbContext.Set<WarningEntity>().Where(l => l.UserId == u.Uid).OrderByDescending(w => w.Timestamp).FirstOrDefault().Comment,
                            };

            var user = await userQuery.FirstOrDefaultAsync();
            return user;
        }
    }
}
