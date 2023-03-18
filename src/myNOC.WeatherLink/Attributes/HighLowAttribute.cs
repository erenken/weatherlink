namespace myNOC.WeatherLink.Attributes
{
	public enum HighOrLow
	{
		High,
		Low
	}

	public class HighLowAttribute : Attribute
	{
		public HighLowAttribute(HighOrLow highValue = HighOrLow.High, params string[]? relatedProperties)
		{
			RelatedProperties = relatedProperties;
			HighValue = highValue;
		}

		public string[]? RelatedProperties { get; }
		public HighOrLow HighValue { get; }
	}
}
