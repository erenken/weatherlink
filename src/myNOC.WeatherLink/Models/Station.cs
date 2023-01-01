using System.Text.Json.Serialization;

namespace myNOC.WeatherLink
{
	public class Station
	{
		[JsonPropertyName("station_id")]
		public int Id { get; set; } = default!;
		[JsonPropertyName("station_name")]
		public string Name { get; set; } = default!;
		[JsonPropertyName("product_number")]
		public string ProductNumber { get; set; } = default!;
		[JsonPropertyName("username")]
		public string UserName { get; set; } = default!;
		[JsonPropertyName("user_email")]
		public string UserEmail { get; set; } = default!;
		[JsonPropertyName("company_name")]
		public string CompanyName { get; set; } = default!;
		[JsonPropertyName("active")]
		public bool Active { get; set; }
		[JsonPropertyName("private")]
		public bool Private { get; set; }
		[JsonPropertyName("recording_interval")]
		public int UploadInterval { get; set; }
		[JsonPropertyName("firmware_version")]
		public int? FirmwareVersion { get; set; }
		[JsonPropertyName("imei")]
		public string IMEI { get; set; } = default!;
		[JsonPropertyName("meid")]
		public string MEID { get; set; } = default!;
		[JsonPropertyName("registered_date")]
		public int UnixRegisteredDate { get; set; }
		public DateTimeOffset RegsisteredDate => DateTimeOffset.FromUnixTimeSeconds(UnixRegisteredDate);
		[JsonPropertyName("subscription_end_date")]
		public int UnixSubscriptionEndDate { get; set; }
		public DateTimeOffset SubscriptionEndDate => DateTimeOffset.FromUnixTimeSeconds(UnixSubscriptionEndDate);
		[JsonPropertyName("time_zone")]
		public string TimeZone { get; set; } = default!;
		[JsonPropertyName("city")]
		public string City { get; set; } = default!;
		[JsonPropertyName("region")]
		public string Region { get; set; } = default!;
		[JsonPropertyName("country")]
		public string Country { get; set; } = default!;
		[JsonPropertyName("latitude")]
		public float Latitude { get; set; }
		[JsonPropertyName("longitude")]
		public float Longitude { get; set; }
		[JsonPropertyName("elevation")]
		public float Elevation { get; set; }
	}
}
