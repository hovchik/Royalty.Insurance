using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Royalty.Insurance.SessionScheduler.DataManager;

[assembly: FunctionsStartup(typeof(Royalty.Insurance.SessionScheduler.Startup))]

namespace Royalty.Insurance.SessionScheduler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<RoyaltyInsuranceContext>(
                options => options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString") ?? throw new InvalidOperationException("Invalid ConnectionString")));

        }
    }
}