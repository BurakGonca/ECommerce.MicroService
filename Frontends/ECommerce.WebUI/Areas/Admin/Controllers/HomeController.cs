using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Home")]
	public class HomeController : Controller
    {
		[Route("Index")]
		public IActionResult Index()
        {
			ViewBag.v1 = "Anasayfa";
			return View();
        }
    }
}
