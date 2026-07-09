# .NET 10 Upgrade Plan

## Overview

**Target**: Upgrade myNOC.WeatherLink solution from .NET 8/9 to .NET 10 (LTS)
**Scope**: 3 projects (~2,100 LOC), all SDK-style, modern .NET

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 3 projects, all already on modern .NET (net8.0/net9.0), straightforward TFM bump with package updates and minimal code changes.

### Projects
- **src/myNOC.WeatherLink** — Class library (multi-targeted net8.0;net9.0)
- **samples/SampleConsoleApp** — Console application (multi-targeted net8.0;net9.0)
- **tests/myNOC.Tests.WeatherLink** — Test project (multi-targeted net8.0;net9.0)

## Tasks

### 01-prerequisites: Verify upgrade prerequisites

Verify that the .NET 10 SDK is installed and properly configured before attempting the upgrade. Check for any global.json files that might constrain the SDK version, and ensure they're compatible with .NET 10.

Assessment context: Solution uses modern SDK-style projects. No legacy project formats detected.

**Done when**:
- .NET 10 SDK is verified as installed
- global.json compatibility confirmed (or no constraints found)
- Build tooling is ready for .NET 10 targets

---

### 02-upgrade-all: Upgrade all projects to .NET 10

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

---

### 03-validation: Validate upgrade completion

Run the full test suite to ensure functionality is preserved after the upgrade. Verify that all projects build cleanly and tests pass. Document any behavioral changes observed and any recommendations for future modernization.

Assessment context: Test project uses MSTest 3.7.0 with coverage collection. No breaking changes expected in test framework itself.

**Done when**:
- Full solution builds with zero errors and zero warnings
- All unit tests pass successfully
- No runtime exceptions or unexpected behavioral changes detected
- Any deferred recommendations or warnings are documented
