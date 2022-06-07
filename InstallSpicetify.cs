using System.Diagnostics;
using static cgozenity;
using static Spicetify_Manager.Config;
using static Spicetify_Manager.SpicetifyFunctions;

namespace Spicetify_Manager;

public static class InstallSpicetifyClass
{
    public static void InstallSpicetify()
    {
        bool install = zenQuestion("Spicetify is not installed, do you want to install it?", "Spicetify Manager");
        if (install)
        {
            Process process = Process.Start("sh",
                "-c \"curl -fsSL https://raw.githubusercontent.com/spicetify/spicetify-cli/master/install.sh | sh\"");
            process.WaitForExit();
            MkConfig(false, false, false);
        }
        else
        {
            zenInfo("This program does not work without Spicetify installed", "Spicetify Manager");
            Environment.Exit(0);
        }
    }

    public static void InstallSpicetifyMarketplace()
    {
        bool install =
            zenQuestion(
                "Do you want to install Spicetify Marketplace?\nIt will allow you to use Spicetify within the Spotify app.\nThis is recommended.",
                "Spicetify Manager");
        if (install)
        {
            SpicetifyBackup();
            SpicetifyApply();
            Process process = Process.Start("sh",
                "-c \"curl -fsSL https://raw.githubusercontent.com/spicetify/spicetify-marketplace/main/install.sh | sh\"");
            process.WaitForExit();
            MkConfig(true, false, false);
        }
        else
        {
            MkConfig(false, false, false);
        }
    }
}