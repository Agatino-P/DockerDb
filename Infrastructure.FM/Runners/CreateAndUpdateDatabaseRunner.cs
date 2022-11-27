using FluentMigrator.Runner;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.FM.Runners;

public class CreateAndUpdateDatabaseRunner : BaseRunner
{
    private readonly IMigrationRunner migrationRunner;

    public CreateAndUpdateDatabaseRunner(
        ILogger<CreateAndUpdateDatabaseRunner> logger,
        IMigrationRunner migrationRunner,
        IOptions<DatabaseConfiguration> databaseConfiguration) : base(databaseConfiguration.Value, logger)
    {
        this.migrationRunner = migrationRunner;
    }

    public async Task ExecuteAsync()
    {
        this.logger.LogInformation("Checking if Db exists ...");

        await this.CreateDatabaseIfNeeded();

        const string action = "Migration to latest version";

        using IMigrationScope scope = this.migrationRunner.BeginScope();
        try
        {
            this.logger.LogInformation("Starting {Action}", action);
            this.migrationRunner.MigrateUp();
            this.logger.LogInformation("Success {Action}", action);

            scope.Complete();

            this.logger.LogInformation("Migration complete");

            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            this.logger.LogError(e, "Failure {Action}", action);
            scope.Cancel();
            throw;
        }
    }
}