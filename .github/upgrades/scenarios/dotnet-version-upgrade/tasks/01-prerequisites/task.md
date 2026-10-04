# 01-prerequisites: Verify upgrade prerequisites

Verify that the .NET 10 SDK is installed and properly configured before attempting the upgrade. Check for any global.json files that might constrain the SDK version, and ensure they're compatible with .NET 10.

Assessment context: Solution uses modern SDK-style projects. No legacy project formats detected.

**Done when**:
- .NET 10 SDK is verified as installed
- global.json compatibility confirmed (or no constraints found)
- Build tooling is ready for .NET 10 targets
