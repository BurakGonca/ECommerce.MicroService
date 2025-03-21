using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
	public class _TopBarUILayoutComponentPartial : ViewComponent
	{

		public IViewComponentResult Invoke()
		{
			var user = HttpContext.User;
			if (user.Identity.IsAuthenticated)
			{
				var fullName = user.FindFirst(ClaimTypes.Name)?.Value;
				ViewBag.UserFullName = fullName;
			}

			return View();
		}

	}
}
