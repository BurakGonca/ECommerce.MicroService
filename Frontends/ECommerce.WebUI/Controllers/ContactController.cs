using ECommerce.DtoLayer.CatalogDtos.ContactDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
	public class ContactController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ContactController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddMessage(CreateContactDto createContactDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createContactDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7081/api/Contacts", stringContent);
			if (responseMessage.IsSuccessStatusCode)
				TempData["SuccessMessage"] = "Mesajınız başarıyla iletilmiştir.";
			else
				TempData["WarningMessage"] = "Mesaj gönderilirken bir hata oluştu.";

			return RedirectToAction("Index", "Contact"); 
		}

	}
}
