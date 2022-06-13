using System.Diagnostics;

namespace Spicetify_Manager;

public class SpicetifyFunctions
{
    public static void SpicetifyUpgrade()
    {
        Process process = Process.Start("Spicetify", "upgrade");
        process.WaitForExit();
    }

    public static void SpicetifyBackup()
    {
        Process process = Process.Start("spicetify", "backup");
        process.WaitForExit();
    }

    public static void SpicetifyApply()
    {
        Process process = Process.Start("spicetify", "apply");
        process.WaitForExit();
    }
}