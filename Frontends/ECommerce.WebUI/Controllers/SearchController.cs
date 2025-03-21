using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Controllers
{
	public class SearchController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public SearchController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		[HttpGet]
		public async Task<IActionResult> GetProductBySearchTerm(string searchTerm)
		{
			var url = $"https://localhost:7081/api/Product/GetProductBySearchTerm?searchTerm={searchTerm}";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync(url);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductBySearchTerm>>(jsonData);

				foreach (var item in values)
				{
					item.SearchTerm = searchTerm;
				}

				return View(values);
			}
			return View();


			

			
		}



	}
}
