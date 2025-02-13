using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
	public class _TopBarUILayoutComponentPartial : ViewComponent
	{

		public IViewComponentResult Invoke()
		{
			return View();
		}

	}
}
