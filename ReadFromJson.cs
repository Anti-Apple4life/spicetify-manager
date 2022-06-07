namespace Spicetify_Manager;

using System.Text.Json.Nodes;

public static class ReadFromJson
{
    public static SpicetifyManagerConfig ReadJson()
    {
        SpicetifyManagerConfig config = new SpicetifyManagerConfig();
        // ReSharper disable once InvertIf
        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SpicetifyManager.json")))
        {
            string json =
                File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "SpicetifyManager.json"));
            JsonNode spicetifyNode = JsonNode.Parse(json)!;
            config.IsMarketplaceInstalled = spicetifyNode["IsMarketplaceInstalled"]!.GetValue<bool>();

            config.IsSpicetifyApplied = spicetifyNode["IsSpicetifyApplied"]!.GetValue<bool>();

            config.IsSpicetifyBackedUp = spicetifyNode["IsSpicetifyBackedUp"]!.GetValue<bool>();
        }

        return config;
    }
}