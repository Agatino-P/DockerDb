using Infrastructure.FM.Runners;
using Shared.Tests.Database.TableHelpers;

namespace Shared.Tests.Database.Fixture;

public class DatabaseFixture : IAsyncLifetime
{
    public Configuration Configuration { get; }
    private SqlContainer SqlContainer { get; }

    public DatabaseFixture()
    {
        this.Configuration = new Configuration();
        this.SqlContainer = new SqlContainer();
    }

    public async Task InitializeAsync()
    {
        await this.SqlContainer.InitializeAsync();
        this.Configuration.InjectConnectionString(this.SqlContainer.ConnectionString);
        await this.Configuration.GetRequiredService<CreateAndUpdateDatabaseRunner>().ExecuteAsync();
    }

    public async Task DisposeAsync()
    {
        await this.SqlContainer.DisposeAsync();
    }

    public PeopleTableHelper GetPeopleTableHelper() => new(Configuration.GetNpgsqlConnection());
}
