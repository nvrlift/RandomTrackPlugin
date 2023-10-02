﻿using AssettoServer.Server.Configuration;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace RandomTrackPlugin;

[UsedImplicitly(ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.WithMembers)]
public class RandomTrackConfiguration : IValidateConfiguration<RandomTrackConfigurationValidator>
{
    public List<TrackWeight> TrackWeights { get; init; } = new();

    public int TrackDurationMinutes { get; set; } = 5;
    public bool UpdateContentManager { get; init; } = false;

    [YamlIgnore] public int TrackDurationMilliseconds => TrackDurationMinutes * 60_000;
}

public class TrackWeight
{
    public required string Name { get; init; }
    public required string TrackFolder { get; init; }
    public required string TrackLayoutConfig { get; init; }
    public required float Weight { get; init; }
    public string CMLink { get; init; } = "";
    public string CMVersion { get; init; } = "";
}
