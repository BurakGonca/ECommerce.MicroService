using Newtonsoft.Json.Linq;

namespace ECommerce.WebUI.Helpers
{
	public static class TokenHelper
	{
		public static async Task<string> GetAccessTokenAsync()
		{
			string token = string.Empty;

			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage()
				{
					RequestUri = new Uri("http://localhost:5001/connect/token"),
					Method = HttpMethod.Post,
					Content = new FormUrlEncodedContent(new Dictionary<string, string>
					{
						{ "client_id", "ECommerceVisitorId" },
						{ "client_secret", "ecommercesecret" },
						{ "grant_type", "client_credentials" }
					})
				};

				using (var response = await httpClient.SendAsync(request))
				{
					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						var tokenResponse = JObject.Parse(content);
						token = tokenResponse["access_token"].ToString();
					}
					else
					{
						throw new Exception("Token alırken bir hata oluştu.");
					}
				}
			}

			return token;
		}
	}
}

