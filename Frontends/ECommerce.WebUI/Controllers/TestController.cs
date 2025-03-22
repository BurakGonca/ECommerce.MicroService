using ECommerce.DtoLayer.CatalogDtos.CategoryDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;


namespace ECommerce.WebUI.Controllers
{
	public class TestController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public TestController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		
		
		public async Task<IActionResult> Index()
		{

			string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			//token çözümlemesi
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

			var responseMessage = await client.GetAsync("https://localhost:7081/api/Categories");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
				return View(values);
			}
			return View();
		}



	}
}
