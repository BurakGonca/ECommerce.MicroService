
using ECommerce.DtoLayer.CatalogDtos.ProductImageDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.ProductDetailViewComponents
{
	public class _ProductDetailImageSliderComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseMessage = await client.GetAsync($"https://localhost:7081/api/ProductImages/ProductImagesByProductId?id={id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var value = JsonConvert.DeserializeObject<ResultProductImageDto>(jsonData);
				return View(value);
			}
			return View();
		}
	}
}
