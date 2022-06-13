using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Spicetify_Manager;

public class SpicetifyManagerConfig
{
    public bool IsMarketplaceInstalled { get; set; }
    public bool IsSpicetifyApplied { get; set; }
    public bool IsSpicetifyBackedUp { get; set; }
    public bool IsLinuxMode { get; set; }
}

public static class Config
{
    public static void MkConfig(bool isMarketplaceInstalled, bool isSpicetifyApplied, bool isSpicetifyBackedUp,
        bool isLinuxMode)
    {
        SpicetifyManagerConfig config = new()
        {
            IsMarketplaceInstalled = isMarketplaceInstalled,
            IsSpicetifyApplied = isSpicetifyApplied,
            IsSpicetifyBackedUp = isSpicetifyBackedUp,
            IsLinuxMode = isLinuxMode
        };

        JsonTypeInfo<SpicetifyManagerConfig> typeInfo = SourceGenerationContext.Default.SpicetifyManagerConfig;
        string jsonString = JsonSerializer.Serialize(config, typeInfo);
        string savePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        File.WriteAllText(Path.Combine(savePath, "SpicetifyManager.json"), jsonString);
    }
}

[JsonSerializable(typeof(SpicetifyManagerConfig))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}