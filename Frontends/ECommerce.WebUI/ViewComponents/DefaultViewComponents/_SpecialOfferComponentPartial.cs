using ECommerce.DtoLayer.CatalogDtos.SpecialOfferDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.DefaultViewComponents
{
	public class _SpecialOfferComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _SpecialOfferComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}



		public async Task<IViewComponentResult> InvokeAsync()
		{

			string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseMessage = await client.GetAsync("https://localhost:7081/api/SpecialOffers");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
				return View(values);
			}
			return View();
		}
	}
}
