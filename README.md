[![Build and Test](https://github.com/erenken/weatherlink/actions/workflows/build-tests.yml/badge.svg)](https://github.com/erenken/weatherlink/actions/workflows/build-tests.yml) [![Release](https://github.com/erenken/weatherlink/actions/workflows/release.yml/badge.svg?branch=main)](https://github.com/erenken/weatherlink/actions/workflows/release.yml) <a href="https://www.nuget.org/packages/myNOC.WeatherLink"><img src="https://img.shields.io/nuget/v/myNOC.WeatherLink.svg" alt="NuGet Version" /></a>
<a href="https://www.nuget.org/packages/myNOC.WeatherLink"><img src="https://img.shields.io/nuget/dt/myNOC.WeatherLink.svg" alt="NuGet Download Count" /></a>

# myNOC.WeatherLink

A library for communicating with the [WeatherLink v2 API](https://weatherlink.github.io/v2-api/).

## Requirements

- .NET 8.0 or .NET 10.0 (both LTS)

## Installation

Install the latest stable [myNOC.WeatherLink package](https://www.nuget.org/packages/myNOC.WeatherLink/):

```sh
dotnet add package myNOC.WeatherLink
```

## Documentation & Usage

The documentation for the library can be found [here](./src/myNOC.WeatherLink/README.md).

## Build and release

Use the latest stable .NET 10 SDK, with the .NET 8 runtime installed to run both test targets:

```sh
dotnet restore myNOC.WeatherLink.sln --warnaserror
dotnet build myNOC.WeatherLink.sln --configuration Release --no-restore --warnaserror
dotnet test myNOC.WeatherLink.sln --configuration Release --no-build --no-restore
```

The [Build and Test workflow](.github/workflows/build-tests.yml) runs for pull requests targeting `main`, pushes to other branches, and manual runs. The [Release workflow](.github/workflows/release.yml) runs on pushes to `main` and can also be run manually on `main`. Both workflows build and test .NET 8 and .NET 10, calculate versions with GitVersion 6.8.2, and pack the library and portable symbols. Only the release workflow publishes to NuGet and creates a GitHub release at the tested commit.

Both workflows treat restore, build, and packaging warnings as errors, so a warning blocks publishing.

### Automatic versioning

Both pipelines fetch the full Git history and tags, then use [GitVersion.yml](GitVersion.yml) to calculate the version. They pass the calculated values to MSBuild for both target frameworks:

| Build or release field | GitVersion value |
| --- | --- |
| `Version`, `PackageVersion`, `.nupkg` and `.snupkg` versions | `SemVer` |
| `AssemblyVersion` | `AssemblySemVer` |
| `FileVersion` | `AssemblySemFileVer` |
| `InformationalVersion` | `InformationalVersion`, including branch and commit metadata |
| GitHub release name and tag | `v` followed by `SemVer` |

Versions are stamped into build artifacts during CI; the pipelines do not rewrite or commit version numbers into project files. GitVersion already includes the commit hash in `InformationalVersion`, so the workflows disable the SDK's extra hash suffix.

On `main`, the default increment is a patch and the version has no prerelease label. The release workflow rejects any calculated version that is not a stable `major.minor.patch` before building or publishing. For example, after a release tagged `v1.2.3`, the next ordinary change builds and publishes `1.2.4`; the release workflow creates `v1.2.4`, which anchors the following increment. A rerun of an already tagged commit keeps its existing version. Publishing skips duplicate packages and existing GitHub releases.

Branches named `work/*` or `work-*` produce `alpha` previews with a minor increment, such as `1.3.0-alpha.2`. Other branches and pull requests use GitHubFlow's branch or pull-request labels. These pipelines pack previews for validation but do not publish them. A work branch's minor preview version does not itself request a minor release on `main`.

To request a different release increment, include `+semver: minor` or `+semver: major` in a commit message that reaches `main`. With squash merges, include the directive in the final squash commit message. GitVersion also recognizes `+semver: patch`. See [GitVersion's version incrementing documentation](https://gitversion.net/docs/reference/version-increments).

### Source Link and symbols

The library uses the [Source Link support included in the .NET SDK](https://github.com/dotnet/sourcelink#using-source-link-in-net-projects). Deterministic builds produce portable PDBs with GitHub source mappings pinned to the exact build commit. The NuGet manifest includes the repository URL and commit, and generated or untracked source files are embedded in the PDBs.

The `.snupkg` contains symbols for both .NET 8 and .NET 10. Both workflows extract this final symbol package and run `sourcelink test` on each PDB to download the mapped sources and verify their checksums. A failed check blocks publishing. The pinned Source Link CLI runs from a temporary runner directory with runtime roll-forward enabled; it is not a package dependency for consumers.

Local packages built from an unpushed commit cannot download that commit's source from GitHub until it is pushed. The CI check runs against the checked-out GitHub commit. See the [package README's debugging instructions](src/myNOC.WeatherLink/README.md#debugging-with-source-link) for symbol server setup.

### NuGet Trusted Publishing setup

NuGet publishing uses [Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing). Before running the release workflow, configure the package owner's nuget.org account:

1. Add a GitHub Trusted Publishing policy with repository owner `erenken`, repository `weatherlink`, and workflow file `release.yml` (file name only).
2. Leave the policy environment empty; this workflow does not use a GitHub environment.
3. Scope the policy to `myNOC.WeatherLink`.
4. Set the GitHub repository Actions secret `NUGET_USER` to the nuget.org **username**, not an email address or organization name. The username must own the policy and have permission to publish the package.

The workflow requests an OIDC token using `id-token: write`, exchanges it through `NuGet/login`, then immediately publishes the package and its `.snupkg` symbols using the short-lived API key. The old `NUGET_PUBLISH` secret is no longer used and can be removed after the first successful trusted publish. `contents: write` lets the workflow create the GitHub release and tag.
