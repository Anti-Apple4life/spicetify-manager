namespace Spicetify_Manager;
using static cgozenity;
using static UrlOpener;
using static InstallSpicetifyClass;

public static class Program
{

    public static async Task Main(string[] args)
    {
        // unfinished
        await CheckAndAlertUpdate();
        zenInfo("Welcome to Spicetify Manager!", "Spicetify Manager");
        if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".spicetify")) == false)
        {
            InstallSpicetify();
        }
    }

    private static async Task CheckAndAlertUpdate()
    {
        GithubVersion ghVersion = await UpdateChecker.CheckGitHubNewerVersion();
        if (ghVersion.IsGithubNewer)
        {
            bool update = zenQuestion("Version " + ghVersion.LatestGithubVersion + " is available. Do you want to update?\nCurrent version: " + ghVersion.LocalVersion, "Updater");
            if (update)
            {
                OpenUrl("https://github.com/Anti-Apple4life/spicetify-manager/releases/latest");
                Environment.Exit(0);
            }
        }

    }
}