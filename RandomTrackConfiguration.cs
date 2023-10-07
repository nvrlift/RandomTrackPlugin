using AssettoServer.Server.Configuration;
using JetBrains.Annotations;
using nvrlift.AssettoServer.Track;
using VotingTrackPlugin;
using YamlDotNet.Serialization;

namespace RandomTrackPlugin;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class RandomTrackConfiguration : NvrliftBaseConfiguration, IValidateConfiguration<RandomTrackConfigurationValidator>
{
    public List<WeightEntry> TrackWeights { get; init; } = new();

    public int TrackDurationMinutes { get; set; } = 30;
    public int TransitionDurationMinutes { get; set; } = 5;

    [YamlIgnore] public int TrackDurationMilliseconds => TrackDurationMinutes * 60_000;
    [YamlIgnore] public int TransitionDurationMilliseconds => TransitionDurationMinutes * 60_000;

    [YamlIgnore]
    public List<RandomTrackType> RandomTrackTypes => TrackWeights.Select(w => new RandomTrackType(w)).ToList();
}

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public struct WeightEntry
{
    public string Name { get; init; }
    public string TrackFolder { get; init; }
    public string TrackLayoutConfig { get; init; }
    public string? CMLink { get; init; }
    public string? CMVersion { get; init; }
    public float? Weight { get; init; }
}
