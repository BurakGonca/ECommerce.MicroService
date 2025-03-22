using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
	public class LogoutController : Controller
	{
		public async Task<IActionResult> Index()
		{
			await HttpContext.SignOutAsync();
			TempData["Logout"] = "Cikis Yapilmistir";
			return RedirectToAction("SignIn", "Login");
		}
	}
}
