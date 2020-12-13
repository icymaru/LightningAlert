using System.Text.Json;

namespace LightningAlert.Utils
{
	public static class Constants
	{
		public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions(JsonSerializerDefaults.Web);
	}
}
