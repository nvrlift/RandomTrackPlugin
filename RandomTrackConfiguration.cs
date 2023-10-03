using AssettoServer.Server.Configuration;
using JetBrains.Annotations;
using RandomTrackPlugin.Track;
using YamlDotNet.Serialization;

namespace RandomTrackPlugin;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class RandomTrackConfiguration : IValidateConfiguration<RandomTrackConfigurationValidator>
{
    public List<TrackType> TrackWeights { get; init; } = new();

    public int TrackDurationMinutes { get; set; } = 30;
    public int TransitionDurationMinutes { get; set; } = 5;
    public bool UpdateContentManager { get; init; } = false;

    [YamlIgnore] public int TrackDurationMilliseconds => TrackDurationMinutes * 60_000;
    [YamlIgnore] public int TransitionDurationMilliseconds => TransitionDurationMinutes * 60_000;
}
