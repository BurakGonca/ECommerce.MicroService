using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Kategoriler";
			ViewBag.v3 = "Kategori Listesi";
			ViewBag.v0 = "Kategori İşlemleri";
			return View();
		}
	}
}
