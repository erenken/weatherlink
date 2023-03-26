namespace myNOC.WeatherLink.Attributes
{
	public enum HighOrLow
	{
		High,
		Low
	}

	public class HighLowAttribute : Attribute
	{
		public HighLowAttribute(HighOrLow highOrLow = HighOrLow.High, params string[]? relatedProperties)
		{
			RelatedProperties = relatedProperties;
			HighOrLow = highOrLow;
		}

		public string[]? RelatedProperties { get; }
		public HighOrLow HighOrLow { get; }
	}
}
