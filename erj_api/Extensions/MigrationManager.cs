
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentMigrator.Runner;
namespace erj_api.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return host;
        }
    }

}
