namespace Spicetify_Manager;

public class ExtraStuff
{
    public static Version FixVersion(string versionToFix)
    {
        string unfixedVersion = versionToFix;
        string[] charsToRemove = new[] { "v" };
        foreach (string c in charsToRemove)
        {
            unfixedVersion = unfixedVersion.Replace(c, string.Empty);
        }

        Version fixedVersion = new Version(unfixedVersion);
        return fixedVersion;
    }
}