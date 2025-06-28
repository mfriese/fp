using System.Reflection;

namespace Fp.App;

public static class AppSettings
{
    public static string ApiUrl => Receive("apiUrl");

    private static string Receive(string key)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resName = assembly.GetManifestResourceNames()
            ?.FirstOrDefault(r => r.EndsWith("settings.json", StringComparison.OrdinalIgnoreCase));

        using var file = assembly.GetManifestResourceStream(resName!);
        var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(file!);
        if (dict!.TryGetValue(key, out string? value))
            return value;

        return string.Empty;
    }
}
