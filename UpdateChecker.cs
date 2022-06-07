using System.Diagnostics;
using Octokit;

namespace Spicetify_Manager;

using static ExtraStuff;

public static class UpdateChecker
{
    public static async Task<GithubVersion> CheckGitHubNewerVersion()
    {
        GithubVersion githubVersion = new GithubVersion();
        //Get all releases from GitHub
        //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/
        GitHubClient client = new GitHubClient(new ProductHeaderValue("SpicetifyManagerUpdateCheck"));
        IReadOnlyList<Release> releases =
            await client.Repository.Release.GetAll("Anti-Apple4life", "spicetify-manager");

        //Setup the versions
        Version latestGitHubVersion = new Version(releases[0].TagName);
        Version localVersion = new Version("1.0.0"); //Replace this with your local version. 
        //Only tested with numeric values.

        //Compare the Versions
        //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
        int versionComparison = localVersion.CompareTo(latestGitHubVersion);
        switch (versionComparison)
        {
            case < 0:
                //The version on GitHub is more up to date than this local release.
                githubVersion.IsGithubNewer = true;
                githubVersion.IsBeta = false;
                githubVersion.IsEqualToGithub = false;
                githubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
                githubVersion.LocalVersion = localVersion.ToString();
                break;
            case > 0:
                //This local version is greater than the release version on GitHub.
                githubVersion.IsBeta = true;
                githubVersion.IsEqualToGithub = false;
                githubVersion.IsGithubNewer = false;
                githubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
                githubVersion.LocalVersion = localVersion.ToString();
                break;
            default:
                //This local Version and the Version on GitHub are equal.
                githubVersion.IsEqualToGithub = true;
                githubVersion.IsBeta = false;
                githubVersion.IsGithubNewer = false;
                githubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
                githubVersion.LocalVersion = localVersion.ToString();
                break;
        }

        return githubVersion;
    }

    public static async Task<GithubVersion?> CheckSpicetifyUpdate()
    {
        if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".spicetify")) == false)
        {
            Console.WriteLine("Spicetify is not installed, cannot check version");
            return null;
        }

        //Get all releases from GitHub
        //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/
        GitHubClient client = new GitHubClient(new ProductHeaderValue("SpicetifyUpdateCheck"));
        IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("spicetify", "spicetify-cli");

        //Setup the versions

        Version latestGitHubVersion = FixVersion(releases[0].TagName);
        string output;
        using (Process process = new Process())
        {
            process.StartInfo.FileName = "spicetify";
            process.StartInfo.Arguments = "--version";
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            // Synchronously read the standard output of the spawned process.
            StreamReader reader = process.StandardOutput;
            // ReSharper disable once RedundantAssignment
            output = await reader.ReadToEndAsync();

            // Write the redirected output to this application's window.

            await process.WaitForExitAsync();
        }

        Version localVersion = new Version(output); //Replace this with your local version. 
        //Only tested with numeric values.

        //Compare the Versions
        //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
        int versionComparison = localVersion.CompareTo(latestGitHubVersion);
        GithubVersion spicetifyGithubVersion = new GithubVersion();
        switch (versionComparison)
        {
            case < 0:
                //The version on GitHub is more up to date than this local release.

                spicetifyGithubVersion.IsGithubNewer = true;
                spicetifyGithubVersion.IsBeta = false;
                spicetifyGithubVersion.IsEqualToGithub = false;
                spicetifyGithubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
                spicetifyGithubVersion.LocalVersion = localVersion.ToString();
                break;
            case > 0:
                //This local version is greater than the release version on GitHub.
                spicetifyGithubVersion.IsBeta = true;
                spicetifyGithubVersion.IsEqualToGithub = false;
                spicetifyGithubVersion.IsGithubNewer = false;
                spicetifyGithubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
                spicetifyGithubVersion.LocalVersion = localVersion.ToString();
                break;
            default:
                //This local Version and the Version on GitHub are equal.
                spicetifyGithubVersion.IsEqualToGithub = true;
                spicetifyGithubVersion.IsBeta = false;
                spicetifyGithubVersion.IsGithubNewer = false;
                spicetifyGithubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
                spicetifyGithubVersion.LocalVersion = localVersion.ToString();
                break;
        }

        return spicetifyGithubVersion;
    }
}