using System;
using Feit.Areas.Identity.Data;
using Feit.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Feit.Areas.Identity.IdentityHostingStartup))]
namespace Feit.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FeitContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FeitContext")));

                
            });
        }
    }
}