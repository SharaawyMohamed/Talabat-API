using System.Text.Json.Serialization;

namespace E_Commerce.Core.Models.Order
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum PaymentStatus
	{
		Pending,
		Failed,
		Received
	}
}