using nvrlift.AssettoServer.Track;

namespace RandomTrackPlugin;

public class RandomTrackType : TrackBaseType
{
    public float Weight { get; set; } = 1.0f;
    public RandomTrackType(WeightEntry input)
    {
        Name = input.Name;
        TrackFolder = input.TrackFolder;
        TrackLayoutConfig = input.TrackLayoutConfig;
        CMLink = input.CMLink ?? "";
        CMVersion = input.CMVersion ?? "";
        Weight = input.Weight ?? 1.0f;
    }
}
