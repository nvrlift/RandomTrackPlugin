# RandomTrackPlugin
Plugin that changes tracks randomly.

## Configuration
Enable the plugin in `extra_cfg.yml`
```yaml
EnablePlugins:
- RandomTrackPlugin
```

Example configuration (add to bottom of `extra_cfg.yml`)  
```yaml
---
!RandomTrackConfiguration
# Duration until next track change
# Minimum is 5, Default is 30
TrackDurationMinutes: 90
# Transition duration until next track change, server will restart after
# Minimum is 1, Default is 5
TransitionDurationMinutes: 10
# Should content manager download links be updated
UpdateContentManager: true
# Weights for random track selection, setting a weight to 0 blacklists a track, default weight is 1.
TrackWeights:
- DisplayName: Gunsai
  TrackFolder: some/path/to/gunsai
  TrackLayoutConfig: GunsaiTogue
  Weight: 2.0
  CMLink: https://mega.nz/...... # field only required with UpdateContentManager: true
  CMVersion: 1.5 # field only required with UpdateContentManager: true
- DisplayName: Shutoko
  TrackFolder: some/path/to/Shutoko
  TrackLayoutConfig: Default
  Weight: 3.0
  CMLink: https://mega.nz/...... # field only required with UpdateContentManager: true
  CMVersion: 1.5 # field only required with UpdateContentManager: true

```