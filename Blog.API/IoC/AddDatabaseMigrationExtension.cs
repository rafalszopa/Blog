using Blog.Persistence.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.API.IoC
{
    public static class AddDatabaseMigrationExtension
    {
        public static void AddDatabaseMigration(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services
                .AddFluentMigratorCore()
                .ConfigureRunner(configure =>
                    configure
                        .AddSqlServer()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(CreateInitialSchema).Assembly).For.Migrations());
        }
    }
}
