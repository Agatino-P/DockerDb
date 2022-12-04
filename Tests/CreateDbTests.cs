using FluentAssertions;
using Shared.Tests.Database.Fixture;
using Shared.Tests.Database.TableHelpers;

namespace Shared.Tests;

public class CreateDbTests :  IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public CreateDbTests(DatabaseFixture fixture)
    {
        Configuration configuration = fixture.Configuration;
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldStartContainerAndCreateDbAsync()
    {
        PeopleTableHelper peopleTableHelper = _fixture.GetPeopleTableHelper();
        Func<Task> cleanup = () => peopleTableHelper.CleanupAsync();
        await cleanup.Should().NotThrowAsync();
    }
}