using AssettoServer.Server.Plugin;
using Autofac;
using nvrlift.AssettoServer.ContentManager;
using nvrlift.AssettoServer.Track;

namespace RandomTrackPlugin;

public class RandomTrackModule : AssettoServerModule<RandomTrackConfiguration>
{
    protected override void Load(ContainerBuilder builder)
    {
        // Register Base Stuff
        builder.RegisterType<TrackImplementation>().AsSelf().SingleInstance();
        builder.RegisterType<TrackManager>().AsSelf().SingleInstance();
        builder.RegisterType<ContentManagerImplementation>().AsSelf().SingleInstance();

        builder.RegisterType<RandomTrackPlugin>().AsSelf().As<IAssettoServerAutostart>().SingleInstance();
    }
}
