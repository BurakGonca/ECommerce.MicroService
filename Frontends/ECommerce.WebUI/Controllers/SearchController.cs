using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
			var url = $"http://localhost:7081/api/Product/GetProductBySearchTerm?searchTerm={searchTerm}";

			//string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
