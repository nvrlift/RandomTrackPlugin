using AssettoServer.Server;
using AssettoServer.Server.Configuration;
using AssettoServer.Server.Plugin;
using AssettoServer.Shared.Network.Packets.Shared;
using AssettoServer.Shared.Services;
using Microsoft.Extensions.Hosting;
using nvrlift.AssettoServer.Track;
using Serilog;

namespace RandomTrackPlugin;

public class RandomTrackPlugin : CriticalBackgroundService, IAssettoServerAutostart
{
    private struct TrackWeight
    {
        internal RandomTrackType Track { get; init; }
        internal float PrefixSum { get; init; }
    }

    private readonly EntryCarManager _entryCarManager;
    private readonly RandomTrackConfiguration _configuration;
    private readonly TrackManager _trackManager;
    private readonly List<TrackWeight> _tracks = new();

    public RandomTrackPlugin(RandomTrackConfiguration configuration, ACServerConfiguration acServerConfiguration,
        EntryCarManager entryCarManager, TrackManager trackManager,
        IHostApplicationLifetime applicationLifetime) : base(applicationLifetime)
    {
        _configuration = configuration;
        _entryCarManager = entryCarManager;
        _trackManager = trackManager;

        _trackManager.SetTrack(new TrackData(new RandomTrackType()
        {
            Name = _configuration.RandomTrackTypes.FirstOrDefault(t =>
                       t.TrackFolder == acServerConfiguration.Server.Track
                       && t.TrackLayoutConfig == acServerConfiguration.Server.TrackConfig)?.Name
                   ?? acServerConfiguration.Server.Track.Split('/').Last(),
            TrackFolder = acServerConfiguration.Server.Track,
            TrackLayoutConfig = acServerConfiguration.Server.TrackConfig,
            CMLink = _configuration.RandomTrackTypes.FirstOrDefault(t =>
                t.TrackFolder == acServerConfiguration.Server.Track
                && t.TrackLayoutConfig == acServerConfiguration.Server.TrackConfig)?.CMLink ?? "",
            CMVersion = _configuration.RandomTrackTypes.FirstOrDefault(t =>
                t.TrackFolder == acServerConfiguration.Server.Track
                && t.TrackLayoutConfig == acServerConfiguration.Server.TrackConfig)?.CMVersion ?? ""
        }, null)
        {
            IsInit = true,
            ContentManager = _configuration.ContentManager,
            TransitionDuration = 0
        });

        float weightSum = _configuration.RandomTrackTypes
            .Select(w => w.Weight)
            .Sum();

        float prefixSum = 0.0f;
        foreach (var track in _configuration.RandomTrackTypes)
        {
            if (track.Weight > 0)
            {
                prefixSum += track.Weight / weightSum;
                _tracks.Add(new TrackWeight
                {
                    Track = track,
                    PrefixSum = prefixSum,
                });
            }
        }

        _tracks.Sort((a, b) =>
        {
            if (a.PrefixSum < b.PrefixSum)
                return -1;
            if (a.PrefixSum > b.PrefixSum)
                return 1;
            return 0;
        });
    }

    private RandomTrackType PickRandom()
    {
        float rng = Random.Shared.NextSingle();
        RandomTrackType track = _tracks[0].Track;

        int begin = 0, end = _tracks.Count;
        while (begin <= end)
        {
            int i = (begin + end) / 2;

            if (_tracks[i].PrefixSum <= rng)
            {
                begin = i + 1;
            }
            else
            {
                end = i - 1;
                track = _tracks[i].Track;
            }
        }

        return track;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(_configuration.TrackDurationMilliseconds, stoppingToken);
            try
            {
                RandomTrackType nextTrack = PickRandom();

                var last = _trackManager.CurrentTrack;

                if (last.Type != nextTrack)
                {
                    _entryCarManager.BroadcastPacket(new ChatMessage
                        { SessionId = 255, Message = $"Next track: {nextTrack.Name}" });
                    _entryCarManager.BroadcastPacket(new ChatMessage
                    {
                        SessionId = 255,
                        Message = $"Track will change in {_configuration.TransitionDurationMinutes} minutes."
                    });

                    // Delay the track switch by configured time delay
                    await Task.Delay(_configuration.TransitionDurationMinutes, stoppingToken);

                    _trackManager.SetTrack(new TrackData(last.Type, nextTrack)
                    {
                        TransitionDuration = _configuration.TransitionDurationMilliseconds,
                        ContentManager = _configuration.ContentManager
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during random track change");
            }
            // finally { }
        }
    }
}
