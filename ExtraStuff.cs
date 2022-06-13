using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Spicetify_Manager;

public static class ExtraStuff
{
    public static Version FixVersion(string versionToFix)
    {
        string unfixedVersion = versionToFix;
        string[] charsToRemove = { "v" };
        foreach (string c in charsToRemove) unfixedVersion = unfixedVersion.Replace(c, string.Empty);

        Version fixedVersion = new(unfixedVersion);
        return fixedVersion;
    }

    public static async Task<bool> SpotifyVersionIsSupported()
    {
        using Process process = new();
        // check if OS is macOS or linux and change the path accordingly
        process.StartInfo.FileName = RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            ? Path.Combine("/", "Applications", "Spotify.app", "Contents", "MacOS", "Spotify")
            : "spotify";
        process.StartInfo.Arguments = "--version";
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();

        // Synchronously read the standard output of the spawned process.
        StreamReader reader = process.StandardOutput;
        // ReSharper disable once RedundantAssignment
        string output = await reader.ReadToEndAsync();

        // Write the redirected output to this application's window.

        await process.WaitForExitAsync();
        bool supported = output.Contains("87");
        return supported;
    }
}