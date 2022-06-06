using System.Diagnostics;

namespace Spicetify_Manager;

public class SpicetifyFunctions
{
    public static void SpicetifyUpgrade()
    {
        Process.Start("Spicetify", "upgrade");
    }
}