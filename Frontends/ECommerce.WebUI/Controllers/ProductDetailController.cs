using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Controllers
{
	public class ProductDetailController : Controller
	{

		public IActionResult Index(string id)
		{

			ViewBag.Id = id;
			return View();
		}
	}
}
