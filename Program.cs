﻿namespace Spicetify_Manager;

using static cgozenity;
using static UrlOpener;
using static InstallSpicetifyClass;
using static SpicetifyFunctions;
using static ReadFromJson;

public static class Program
{
    public static async Task Main(string[] args)
    {
        SpicetifyManagerConfig config = ReadJson();
        // unfinished
        await CheckAndAlertManagerUpdate();
        if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".spicetify")) == false)
        {
            InstallSpicetify();
        }
        if (config.IsMarketplaceInstalled == false)
        {
            InstallSpicetifyMarketplace();
        }
        

        await CheckAndAlertSpicetifyUpdate();
    }

    private static async Task CheckAndAlertManagerUpdate()
    {
        GithubVersion ghVersion = await UpdateChecker.CheckGitHubNewerVersion();
        if (ghVersion.IsGithubNewer)
        {
            bool update =
                zenQuestion(
                    "Version " + ghVersion.LatestGithubVersion +
                    " of Spicetify Manager is available. Do you want to update?\nCurrent manager version: " +
                    ghVersion.LocalVersion, "Updater");
            if (update)
            {
                OpenUrl("https://github.com/Anti-Apple4life/spicetify-manager/releases/latest");
                Environment.Exit(0);
            }
        }
    }

    private static async Task CheckAndAlertSpicetifyUpdate()
    {
        GithubVersion? spicetifyUpdate = await UpdateChecker.CheckSpicetifyUpdate();
        if (spicetifyUpdate!.IsGithubNewer)
        {
            bool update =
                zenQuestion(
                    "Version " + spicetifyUpdate.LatestGithubVersion +
                    " of Spicetify is available. Do you want to update?\nCurrent Spicetify version: " +
                    spicetifyUpdate.LocalVersion, "Updater");
            if (update)
            {
                SpicetifyUpgrade();
            }
        }
    }
}