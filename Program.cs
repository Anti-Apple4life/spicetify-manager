namespace Spicetify_Manager;
using static cgozenity;

public static class Program
{

    public static async Task Main(string[] args)
    {
        // unfinished
        await CheckForUpdate();
    }

    private static async Task CheckForUpdate()
    {
        // unfinished
        GithubVersion ghVersion = await UpdateChecker.CheckGitHubNewerVersion();
        Console.Write(ghVersion.LatestGithubVersion);
    }
}