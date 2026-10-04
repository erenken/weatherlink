# 02-upgrade-all: Upgrade all projects to .NET 10

Update target frameworks from net8.0/net9.0 to net10.0 across all three projects. Update 8 NuGet packages to their .NET 10-compatible versions (Microsoft.Extensions.* packages from 9.0.0 → 10.0.9, System.Text.Json from 9.0.0 → 10.0.9). Address code compatibility issues identified in the assessment.

Assessment context:
- **Package updates**: 8 packages need upgrades (all Microsoft.Extensions.* and System.Text.Json)
- **API compatibility**: 2 binary incompatible APIs require code changes (ConfigurationBinder.GetValue calls in SampleConsoleApp)
- **Behavioral changes**: 19 APIs with behavioral changes detected across the solution, primarily Uri and HttpContent usage
- Technologies detected: MSTest framework, NSubstitute mocking, HTTP client usage

Research starting points:
- Review ConfigurationBinder.GetValue migration guidance (binary breaking change in SampleConsoleApp)
- Verify Uri and HttpContent behavioral changes don't impact WeatherLink client functionality
- Check if test mocking setup (NSubstitute) needs updates for .NET 10

**Done when**:
- All project files updated to target net10.0 (remove net8.0 and net9.0 targets)
- All 8 packages updated to .NET 10-compatible versions
- Solution builds successfully with zero errors
- All compilation errors from API changes have been resolved
