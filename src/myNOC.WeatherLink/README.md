# myNOC.WeatherLink

## Overview

A library for communicating with the [WeatherLink v2 API](https://weatherlink.github.io/v2-api/).

This library is initially built for my needs as I am building a new Weather Website form my station, and in the process I thought this library may be useful to more people.

## Supported Data Structures

This support Current Conditions and Archive [data structures](https://weatherlink.github.io/v2-api/data-structure-types) for WeatherLink Live ISS and AirLink.  I will be adding the WeatherLink Console soon, as I just got one.

## Setup and Configuration

To use `myNOC.WeatherLink` you will need to add it to your `IServiceCollection`

```csharp
services.AddWeatherLink();
```

> `AddWeatherLink` has an overload to either add the `IAPIContext` as a singled or scoped.  The default is that it is added as a singleton.

You will need to setup `ILogger` in your project as the `APIRepository` uses it to log the API Uri and the response.

```csharp
services.AddLogging(options => options.AddConsole());
```

In my [sample console application](https://github.com/erenken/weatherlink/tree/main/samples) I am just using `IServiceProvider` and `GetService<>` to get the instances of the objects I need.

Before using `IClient` you will need to setup the `BaseUri`, `APIKey`, and `APISecret` needed to access the WeatherLink v2 API.

### `BaseUri`

```csharp
var apiHttpClient = serviceProvider.GetService<IAPIHttpClient>()!;
apiHttpClient.BaseUri = "https://api.weatherlink.com/v2";
```

### `APIKey` and `APISecret`

```csharp
var apiContext = serviceProvider.GetService<IAPIContext>()!;
apiContext.APIKey = "{yourApiKey}";
apiContext.APISecret = "{yourApiSecret}";
```

## Calling the API

To call the WeatherLink v2 API you will need and instance of `Client` and you can inject that using `IClient`.

### Getting Station List

To get a list of the stations under your account you will use the `GetStations` method.

```csharp
var apiClient = serviceProvider.GetService<IClient>()!;
var stations = await apiClient.GetStations();
```

This will return `StationResponse` which includes `Stations` of `IEnumerable<Station>`.

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
var apiClient = serviceProvider.GetService<IClient>()!;
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
var converterFactory = serviceProvider.GetService<SensorJsonConverterFactory>();
options.Converters.Add(converterFactory!);

var current = await apiClient.GetCurrent(stationId);
var output = JsonSerializer.Serialize(current, options);
```

This will properly serialize all of the `Sensor<T>` data.  `Data` is of `IEnumerable<ISensorData>`.

#### Deserialize

If you store the data and want to `Deserialize` it back into `WeatherDataResponse` you will again need to use the `SensorJasonConverterFactory`.

```csharp
JsonSerializerOptions options = new();
var converterFactory = serviceProvider.GetService<SensorJsonConverterFactory>();
options.Converters.Add(converterFactory!);

var storedCurrent = GetCurrentFromStorage();
var WeatherDataResponse = JsonSerializer.Deserialize<WeatherDataResponse>(storedCurrent, options);
```
