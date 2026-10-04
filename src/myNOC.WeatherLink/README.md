# myNOC.WeatherLink

## Overview

A library for communicating with the [WeatherLink v2 API](https://weatherlink.github.io/v2-api/).

## Requirements

- .NET 8.0 or .NET 10.0 (both LTS)

This library was built for a weather website using a personal station and is available for other WeatherLink integrations.

## Installation and versions

Install the latest stable package from [NuGet](https://www.nuget.org/packages/myNOC.WeatherLink/):

```sh
dotnet add package myNOC.WeatherLink
```

The package supports both .NET 8 and .NET 10. Stable releases are published from `main` after both targets build and their tests pass. CI calculates package and assembly versions automatically with GitVersion and creates a matching GitHub release tag. Work branches use `alpha` preview versions for validation. See the [repository's versioning and release documentation](https://github.com/erenken/weatherlink/blob/main/README.md#automatic-versioning) for increment rules and NuGet Trusted Publishing setup.

## Debugging with Source Link

NuGet releases include a separate `.snupkg` with portable symbols for .NET 8 and .NET 10. Source Link maps the library's source files to the exact GitHub commit used to build the package, allowing a debugger to download the matching source. The release pipeline verifies source downloads and checksums before publishing.

In Visual Studio:

1. Under **Tools > Options > Debugging > Symbols**, add `https://symbols.nuget.org/download/symbols` as a symbol source.
2. Under **Debugging > General**, enable **Enable Source Link support** and disable **Enable Just My Code** when stepping into the library.
3. Load symbols for `myNOC.WeatherLink.dll` and step into a library method. Symbol availability depends on NuGet finishing validation and indexing of the symbol package.

See Microsoft's [Source Link guidance](https://learn.microsoft.com/en-us/dotnet/standard/library-guidance/sourcelink).

## Supported Data Structures

This library supports current conditions and archive [data structures](https://weatherlink.github.io/v2-api/data-structure-types) for WeatherLink Live ISS and AirLink.

## Setup and Configuration

Register `myNOC.WeatherLink` with your `IServiceCollection`:

```csharp
services.AddWeatherLink();
```

`AddWeatherLink()` registers `IAPIContext` as a singleton by default. Use `services.AddWeatherLink(registerIAuthenticationSingleton: false)` to register the context as scoped. `IClient`, `IAPIRepository`, and `SensorJsonConverterFactory` are scoped services.

You will need to setup `ILogger` in your project as the `APIRepository` uses it to log the API Uri and the response.

```csharp
services.AddLogging(options => options.AddConsole());
```

For a console application, create a scope and resolve services from it. The following examples use this `serviceProvider`:

```csharp
using var rootProvider = services.BuildServiceProvider();
using var scope = rootProvider.CreateScope();
var serviceProvider = scope.ServiceProvider;
```

The [sample console application](https://github.com/erenken/weatherlink/tree/main/samples) reads credentials from the `WL_APIKEY` and `WL_APISECRET` environment variables.

Before using `IClient` you will need to setup the `BaseUri`, `APIKey`, and `APISecret` needed to access the WeatherLink v2 API.

### `BaseUri`

```csharp
var apiHttpClient = serviceProvider.GetRequiredService<IAPIHttpClient>();
apiHttpClient.BaseUri = "https://api.weatherlink.com/v2";
```

### `APIKey` and `APISecret`

```csharp
var apiContext = serviceProvider.GetRequiredService<IAPIContext>();
apiContext.APIKey = "{yourApiKey}";
apiContext.APISecret = "{yourApiSecret}";
```

## Calling the API

To call the WeatherLink v2 API, resolve or inject `IClient`.

### Getting Station List

To get a list of the stations under your account you will use the `GetStations` method.

```csharp
var apiClient = serviceProvider.GetRequiredService<IClient>();
var stations = await apiClient.GetStations();
```

This returns `StationsResponse`, which includes a `Stations` property of type `IEnumerable<Station>`.

```json
{
    "stations": [
        {
            "station_id": 1234,
            "station_name": "Milton Township",
            "product_number": "6100",
            "username": "username",
            "user_email": "user@email.com",
            "company_name": "",
            "active": true,
            "private": false,
            "recording_interval": 15,
            "firmware_version": null,
            "imei": null,
            "meid": null,
            "registered_date": 1588082732,
            "RegsisteredDate": "2020-04-28T14:05:32+00:00",
            "subscription_end_date": 0,
            "SubscriptionEndDate": "1970-01-01T00:00:00+00:00",
            "time_zone": "America/Detroit",
            "city": "Niles",
            "region": "MI",
            "country": "United States of America",
            "latitude": 41.000,
            "longitude": -86.000,
            "elevation": 839.25586
        }
    ],
    "generated_at": 1672536462,
    "GeneratedAt": "2023-01-01T01:27:42+00:00"
}
```

### Getting Current Conditions

Once you have your `station_id` you can then pass that into the `GetCurrent` method to get the current sensor readings.

```csharp
var apiClient = serviceProvider.GetRequiredService<IClient>();
var current = await apiClient.GetCurrent(stationId);
```

This will return `WeatherDataResponse` which includes a property `Sensors` of `IEnumerable<Sensor>`.  

```json
{
    "station_id": 88769,
    "sensors": [
        {
            "lsid": 307588,
            "sensor_type": 46,
            "data_structure_type": 10
        },
        {
            "lsid": 446594,
            "sensor_type": 323,
            "data_structure_type": 16
        }
    ],
    "generated_at": 1672536813,
    "GeneratedAt": "2023-01-01T01:33:33+00:00"
}
```

To properly serialize the response from `GetCurrent` you will need to use the `SensorJsonConverterFactory`.

```csharp
JsonSerializerOptions options = new();
var converterFactory = serviceProvider.GetRequiredService<SensorJsonConverterFactory>();
options.Converters.Add(converterFactory);

var current = await apiClient.GetCurrent(stationId);
var output = JsonSerializer.Serialize(current, options);
```

This will properly serialize all of the `Sensor<T>` data.  `Data` is of `IEnumerable<ISensorData>`.

#### Deserialize

If you store the data and want to deserialize it back into `WeatherDataResponse`, use `SensorJsonConverterFactory` again.

```csharp
JsonSerializerOptions options = new();
var converterFactory = serviceProvider.GetRequiredService<SensorJsonConverterFactory>();
options.Converters.Add(converterFactory);

var storedCurrent = GetCurrentFromStorage();
var WeatherDataResponse = JsonSerializer.Deserialize<WeatherDataResponse>(storedCurrent, options);
```
