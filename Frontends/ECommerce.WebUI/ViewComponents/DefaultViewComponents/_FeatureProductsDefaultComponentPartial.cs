using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.ViewComponents.DefaultViewComponents
{
	public class _FeatureProductsDefaultComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _FeatureProductsDefaultComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		
		public async Task<IViewComponentResult> InvokeAsync()
		{

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7081/api/Product/ProductListWithCategory");


			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
				foreach (var product in values)
				{
					product.Category.CategoryName = GetCategoryClass(product.Category?.CategoryName ?? "others");
				}
				return View(values);
			}
			return View();
		}

		private static string GetCategoryClass(string categoryName)
		{
			return categoryName.ToLower() switch
			{
				"kadın" => "women",
				"erkek" => "men",
				"çantalar" => "bag",
				"ayakkabılar" => "shoes",
				"saatler" => "watches",
				_ => "others"
			};
		}


	}
}
