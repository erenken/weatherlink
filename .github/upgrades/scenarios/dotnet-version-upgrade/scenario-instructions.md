# .NET 10 Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0

## Source Control
- **Source Branch**: main
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: Single Commit at End
- **Branch Sync**: Auto (Merge)

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

## Strategy
**Selected**: All-at-Once
**Rationale**: 3 projects, all on modern .NET (net8.0/net9.0), low complexity with straightforward TFM bump and package updates

### Execution Constraints
- Single atomic upgrade — all projects updated together in one pass
- Update project files → update packages → restore → build and fix compilation errors (one bounded pass, not a retry loop)
- Testing only after the atomic upgrade completes successfully
- Validate full solution build with zero errors before marking complete
