using ECommerce.DtoLayer.CatalogDtos.ProductDetailDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7081/api/ProductDetails/ProductDetailsByProductId?id={id}");

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
