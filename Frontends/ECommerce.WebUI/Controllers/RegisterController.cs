using ECommerce.DtoLayer.IdentityDtos.RegisterDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public RegisterController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
		{
			if (createRegisterDto.Password != createRegisterDto.ConfirmPassword)
			{
				TempData["Error"] = "Sifreleriniz birbiriyle uyusmuyor";
				return View();
			}

			if (!createRegisterDto.AgreeTerm)
			{
				TempData["Error"] = "Hizmet sartlarını onaylamalısınız";
				return View();
			}

			var password = createRegisterDto.Password;

			if (!password.Any(char.IsUpper))
			{
				TempData["Error"] = "Şifrelerde en az bir büyük harf ('A'-'Z') bulunmalıdır.";
				return View();
			}

			if (!password.Any(char.IsLower))
			{
				TempData["Error"] = "Şifrelerde en az bir küçük harf ('a'-'z') bulunmalıdır.";
				return View();
			}

			if (!password.Any(c => !char.IsLetterOrDigit(c)))
			{
				TempData["Error"] = "Şifrelerde en az bir alfanümerik olmayan karakter bulunmalıdır.";
				return View();
			}

			var userRegisterDto = new UserRegisterDto
			{
				Username = $"{createRegisterDto.Name}_{createRegisterDto.Surname}".ToLower(),
				Email = createRegisterDto.Email,
				Name = createRegisterDto.Name,
				Surname = createRegisterDto.Surname,
				Password = createRegisterDto.Password
			};

			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(userRegisterDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				TempData["Info"] = "Kayıt isleminiz basariyla gerceklesmistir.";
				return RedirectToAction("Index", "Register"); //sonrasında logine yönlendirecegim
			}
			else
			{
				TempData["Error"] = "Bir hata olustu,tekrar deneyiniz";
				return View();
			}
		}
	}
}
