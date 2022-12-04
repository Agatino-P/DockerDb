using System.Collections.ObjectModel;
using System.Reflection;

namespace ContainersGateway;
public class ScriptExecuter
{
    const string _scriptExtension = ".sql";

    private Dictionary<string, string> _scripts = new();
    private readonly Assembly _assembly;
    private readonly string _scriptsPath;

    public ScriptExecuter(Assembly assembly, string scriptsPath)
    {
        _assembly = assembly;
        _scriptsPath = scriptsPath;
    }

    public ReadOnlyDictionary<string, string> Scripts => new(_scripts);

    public void RetrieveScripts()
    {
        var scriptNames = _assembly.GetManifestResourceNames().Where(r => r.StartsWith(_scriptsPath) && r.EndsWith(_scriptExtension));

        _scripts.Clear();
        foreach (var scriptName in scriptNames)
        {
            string content = readResource(scriptName);
            _scripts.Add(scriptName.Substring(_scriptsPath.Length + 1), content );
        }
    }

    private string readResource(string resouceName)
    {
        using Stream stream = _assembly.GetManifestResourceStream(resouceName);
        using StreamReader reader = new StreamReader(stream);
        string result = reader.ReadToEnd();
        return result;
    }
}
