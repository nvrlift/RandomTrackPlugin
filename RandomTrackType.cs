using nvrlift.AssettoServer.Track;

namespace RandomTrackPlugin;

public class RandomTrackType : ITrackBaseType
{
    public string Name { get; set; }
    public string TrackFolder { get; set; }
    public string TrackLayoutConfig { get; set; }
    public string CMLink { get; set; }
    public string CMVersion { get; set; }
    public float Weight { get; set; }
    public RandomTrackType(WeightEntry input)
    {
        Name = input.Name;
        TrackFolder = input.TrackFolder;
        TrackLayoutConfig = input.TrackLayoutConfig;
        CMLink = input.CMLink ?? "";
        CMVersion = input.CMVersion ?? "";
        Weight = input.Weight ?? 1.0f;
    }
    public RandomTrackType(){}
}
