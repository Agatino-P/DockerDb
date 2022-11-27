using FluentMigrator.Runner;
using Microsoft.Extensions.Logging;

namespace Infrastructure.FM.Runners;

public class RollbackDatabaseRunner
{
    private readonly ILogger<RollbackDatabaseRunner> logger;
    private readonly IMigrationRunner migrationRunner;

    public RollbackDatabaseRunner(ILogger<RollbackDatabaseRunner> logger, IMigrationRunner migrationRunner)
    {
        this.logger = logger;
        this.migrationRunner = migrationRunner;
    }

    public async Task ExecuteAsync(long versionToRollbackTo)
    {
        const string action = "Rollback to version";

        using IMigrationScope scope = this.migrationRunner.BeginScope();
        try
        {
            this.logger.LogInformation("Starting {Action}", action);
            this.migrationRunner.RollbackToVersion(versionToRollbackTo);
            this.logger.LogInformation("Success {Action}", action);

            scope.Complete();

            this.logger.LogInformation("Rollback done");

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