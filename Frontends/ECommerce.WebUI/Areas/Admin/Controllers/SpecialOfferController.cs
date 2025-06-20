using ECommerce.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/SpecialOffer")]
	public class SpecialOfferController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public SpecialOfferController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Özel Teklifler";
			ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";
			ViewBag.v0 = "Özel Teklif İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("http://localhost:7081/api/SpecialOffers");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpGet]
		[Route("CreateSpecialOffer")]
		public IActionResult CreateSpecialOffer()
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Özel Teklifler";
			ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";
			ViewBag.v0 = "Özel Teklif İşlemleri";
			return View();
		}

		[HttpPost]
		[Route("CreateSpecialOffer")]
		public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createSpecialOfferDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("http://localhost:7081/api/SpecialOffers", stringContent);
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
			return View();
		}

		[Route("DeleteSpecialOffer/{id}")]
		public async Task<IActionResult> DeleteSpecialOffer(string id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"http://localhost:7081/api/SpecialOffers?id={id}");
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
			return View();
		}



		[Route("UpdateSpecialOffer/{id}")]
		[HttpGet]
		public async Task<IActionResult> UpdateSpecialOffer(string id)
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Özel Teklifler";
			ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";
			ViewBag.v0 = "Özel Teklif İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"http://localhost:7081/api/SpecialOffers/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(jsonData);
				return View(values);
			}
			return View();
		}


		[Route("UpdateSpecialOffer/{id}")]
		[HttpPost]
		public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
		{


			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateSpecialOfferDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("http://localhost:7081/api/SpecialOffers/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
			return View();
		}





	}
}
