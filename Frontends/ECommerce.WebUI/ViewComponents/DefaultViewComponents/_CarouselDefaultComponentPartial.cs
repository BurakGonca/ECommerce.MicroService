using ECommerce.DtoLayer.CatalogDtos.FeatureSliderDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.DefaultViewComponents
{
	public class _CarouselDefaultComponentPartial : ViewComponent
	{

		private readonly IHttpClientFactory _httpClientFactory;

		public _CarouselDefaultComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		

		public async Task<IViewComponentResult> InvokeAsync()
		{
			//string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseMessage = await client.GetAsync("http://localhost:7081/api/FeatureSliders");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
				return View(values);
			}
			return View();
		}
	}
}
