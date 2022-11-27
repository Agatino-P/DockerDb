using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.FM.Runners;
public class BaseRunner
{
    private readonly DatabaseConfiguration databaseConfiguration;
    protected readonly ILogger<BaseRunner> logger;

    public BaseRunner(DatabaseConfiguration databaseConfiguration, ILogger<BaseRunner> logger)
    {
        this.databaseConfiguration = databaseConfiguration;
        this.logger = logger;
    }

    protected async Task CreateDatabaseIfNeeded()
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new(this.databaseConfiguration.ConnectionString);
        string databaseName = connectionStringBuilder.Database!;

        connectionStringBuilder.Database = "postgres";
        await using NpgsqlConnection sqlConnection = new(connectionStringBuilder.ToString());
        await sqlConnection.OpenAsync();
        bool databaseExists = await DoesDatabaseExist(sqlConnection, databaseName);
        if (!databaseExists)
        {
            await this.CreateDatabase(sqlConnection, databaseName);
        }
    }

    private async static Task<bool> DoesDatabaseExist(NpgsqlConnection sqlConnection, string databaseName)
    {
        string dbExistsQuery = $"select exists(SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower('{databaseName}'))";
        await using NpgsqlCommand sqlCmd = sqlConnection.CreateCommand();
        sqlCmd.CommandText = dbExistsQuery;
        object? dbExists = await sqlCmd.ExecuteScalarAsync();
        return Convert.ToBoolean(dbExists);
    }

    private async Task CreateDatabase(NpgsqlConnection sqlConnection, string databaseName)
    {
        this.logger.LogInformation("Creating Db {Database} ...", databaseName);
        await using NpgsqlCommand sqlCmd = sqlConnection.CreateCommand();
        sqlCmd.CommandText = $"CREATE DATABASE \"{databaseName}\"";
        int result = await sqlCmd.ExecuteNonQueryAsync();
        if (result == -1)
        {
            this.logger.LogInformation("Db {Database} successfully created !", databaseName);
        }
    }
}
