using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.ViewComponents.ProductDetailViewComponents
{
	public class _SimilarProductsSameCategoryComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _SimilarProductsSameCategoryComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			var product = await GetProductByIdAsync(id);
			if (product is null)
				return View();

			var categoryProducts = await GetProductsByCategoryIdAsync(product.Category.CategoryID);
			return View(categoryProducts ?? new List<ResultProductWithCategoryDto>());
		}

		private async Task<ResultProductWithCategoryDto> GetProductByIdAsync(string productId)
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync($"https://localhost:7081/api/Product/{productId}");

			if (!response.IsSuccessStatusCode)
				return null;

			var jsonData = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<ResultProductWithCategoryDto>(jsonData);
		}

		private async Task<List<ResultProductWithCategoryDto>> GetProductsByCategoryIdAsync(string categoryId)
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync($"https://localhost:7081/api/Product/GetProductsWithCategoryByCategoryId?categoryId={categoryId}");

			if (!response.IsSuccessStatusCode)
				return null;

			var jsonData = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
		}

	}
}
