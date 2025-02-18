using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.ViewComponents.ShoppingCartViewComponents
{
	public class _ShoppingCartCartTotalsComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
