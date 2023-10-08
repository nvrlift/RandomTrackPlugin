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
# Should content manager download links be updated
ContentManager: true
Restart: WindowsFile # yet to implement ->Docker
# Duration until next track change
# Minimum is 5, Default is 30
TrackDurationMinutes: 90
# Transition duration until next track change, server will restart after
# Minimum is 1, Default is 5
TransitionDurationMinutes: 5
# Weights for random track selection, setting a weight to 0 blacklists a track, default weight is 1.
# CM field only required with UpdateContentManager: true
TrackWeights:
- { Name: Gunsai, Weight: 2.0, TrackFolder: csp/0/../pk_gunma_cycle_sports_center, TrackLayoutConfig: gcsc_full_attack, CMLink: https://mega.nz/...... , CMVersion: 1.5 }
- { Name: Shutoko, Weight: 2.0, TrackFolder: csp/0/../shuto_revival_project_beta, TrackLayoutConfig: overload_layout, CMLink: https://mega.nz/...... , CMVersion: 1.5 }

```
