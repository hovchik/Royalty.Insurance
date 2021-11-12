using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.SessionScheduler.DataManager;

namespace Royalty.Insurance.SessionScheduler
{
    public class ExpiredSessionProcessor
    {
        private readonly RoyaltyInsuranceContext _context;


        public ExpiredSessionProcessor(RoyaltyInsuranceContext context)
        {
            _context = context;
        }

        [FunctionName("ExpiredSessionProcessor")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                FunctionsAssemblyResolver.RedirectAssembly();
                log.LogInformation($"Timer trigger ExpiredSessionProcessor function executed at: {DateTime.Now}");
                var expiredSessions = await _context.UserActivityLogs
                    .Include(item => item.User)
                    .ThenInclude(item => item.UsersProfile)
                    .Where(
                                                              item =>
                                                                  !item.LogOutDatetimeUtc.HasValue
                                                                  && item.RefreshTokenExpireAt < DateTime.UtcNow)
                    .ToListAsync();
                var expiredUserIds = expiredSessions.Select(item => item.UserId);
                foreach (var expiredSession in expiredSessions)
                {
                    expiredSession.LogOutDatetimeUtc = DateTime.UtcNow;
                    _context.UserActivityLogs.Update(expiredSession);
                    expiredSession.User.UsersProfile.Status = string.Empty;
                    expiredSession.User.UsersProfile.UserStatusId = 2;//offline
                    expiredSession.User.UsersProfile.UserLastStatusId = 2;//offline
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                log.LogError("An error occured when trying to update expired sessions", e);
            }
        }
    }
}
