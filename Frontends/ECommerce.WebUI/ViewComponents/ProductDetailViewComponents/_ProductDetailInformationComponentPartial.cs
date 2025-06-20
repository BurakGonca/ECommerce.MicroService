using ECommerce.DtoLayer.CatalogDtos.ProductDetailDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.ProductDetailViewComponents
{
	public class _ProductDetailInformationComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _ProductDetailInformationComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			//string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseMessage = await client.GetAsync($"http://localhost:7081/api/ProductDetails/ProductDetailsByProductId?id={id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var value = JsonConvert.DeserializeObject<ResultProductDetailDto>(jsonData);
				return View(value);
			}
			return View();
		}
	}
}
