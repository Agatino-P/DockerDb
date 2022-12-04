using FluentAssertions;
using System.Reflection;

namespace ContainersGateway.Tests.ScriptExecutioner;

public class ScriptExecuterTests
{
    private string _scriptPaths = Assembly.GetAssembly(typeof(AssemblyPlaceHolder))!.GetName().Name! + ".ScriptExecutioner.Sql";
    private readonly ScriptExecuter _sut;

    public ScriptExecuterTests()
    {
        _sut = new ScriptExecuter(Assembly.GetExecutingAssembly(), _scriptPaths);
    }

    [Fact]
    public void ShouldRetrieveAllScripts()
    {
        Dictionary<string, string> expectedScripts = new(){
            { "010_Create.sql","CREATE Table;" },
            { "020_Select.sql","SELECT * FROM Table;" }
        };
        Verify(_scriptPaths, expectedScripts);

    }

    private void Verify(string resourcesPath, Dictionary<string, string> expectedScripts)
    {
        _sut.RetrieveScripts();
        _sut.Scripts.Should().BeEquivalentTo(expectedScripts);
    }
}