using Octokit;
namespace Spicetify_Manager;


public class UpdateChecker
{
    public static async Task<GithubVersion> CheckGitHubNewerVersion()
    {
        GithubVersion githubVersion = new GithubVersion();
        //Get all releases from GitHub
        //Source: https://octokitnet.readthedocs.io/en/latest/getting-started/
        GitHubClient client = new GitHubClient(new ProductHeaderValue("SpicetifyManagerUpdateCheck"));
        IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("Anti-Apple4life", "spicetify-manager");

        //Setup the versions
        Version latestGitHubVersion = new Version(releases[0].TagName);
        Version localVersion = new Version("1.0.0"); //Replace this with your local version. 
        //Only tested with numeric values.

        //Compare the Versions
        //Source: https://stackoverflow.com/questions/7568147/compare-version-numbers-without-using-split-function
        int versionComparison = localVersion.CompareTo(latestGitHubVersion);
        if (versionComparison < 0)
        {
            //The version on GitHub is more up to date than this local release.
            githubVersion.IsGithubNewer = true;
            githubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
            githubVersion.LocalVersion = localVersion.ToString();
        }
        else if (versionComparison > 0)
        {
            //This local version is greater than the release version on GitHub.
            githubVersion.IsBeta = true;
            githubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
            githubVersion.LocalVersion = localVersion.ToString();
        }
        else
        {
            //This local Version and the Version on GitHub are equal.
            githubVersion.IsEqualToGithub = true;
            githubVersion.LatestGithubVersion = latestGitHubVersion.ToString();
            githubVersion.LocalVersion = localVersion.ToString();
        }

        return githubVersion;
    }
}