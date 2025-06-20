using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.DefaultViewComponents
{
	public class _FeatureProductsDefaultComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _FeatureProductsDefaultComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		
		/// <summary>
		/// Son eklenen 8 ürüne göre filtreleyerek getiriyor
		/// </summary>
		/// <returns></returns>
		public async Task<IViewComponentResult> InvokeAsync()
		{

			//string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseMessage = await client.GetAsync("http://localhost:7081/api/Product/ProductListWithCategory");


			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData).TakeLast(8).ToList();
				
				return View(values);
			}
			return View();
		}

		


	}
}
