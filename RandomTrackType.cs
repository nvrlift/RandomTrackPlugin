using nvrlift.AssettoServer.Track;

namespace RandomTrackPlugin;

public class RandomTrackType : TrackBaseType
{
    public RandomTrackType(WeightEntry input)
    {
        Name = input.Name;
        
    }
}
