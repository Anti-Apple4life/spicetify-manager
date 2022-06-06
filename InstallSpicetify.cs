using System.Diagnostics;
using static cgozenity;

namespace Spicetify_Manager;

public static class InstallSpicetifyClass
{
    public static void InstallSpicetify()
    {
        bool install = zenQuestion("Spicetify is not installed, do you want to install it?", "Spicetify Manager");
        if (install)
        {
            Process.Start("sh", "-c \"curl -fsSL https://raw.githubusercontent.com/spicetify/spicetify-cli/master/install.sh | sh\"");
        }
        else
        {
            zenInfo("This program does not work without Spicetify installed", "Spicetify Manager");
            Environment.Exit(0);
        }
    }
}