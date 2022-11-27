using FluentAssertions;
using Tests.Database.Fixture;

namespace Tests;

public class CreateDbTests :  IClassFixture<DatabaseFixture>
{
    public CreateDbTests(DatabaseFixture fixture)
    {
        Configuration configuration = fixture.Configuration;
    }

    [Fact]
    public void ShouldStartContainerAndCreateDb()
    {
        true.Should().BeTrue();
    }
}