using ECommerce.DtoLayer.IdentityDtos.LoginDtos;

namespace ECommerce.WebUI.Services.Abstract
{
	public interface IIdentityService
	{
		Task<bool> SignIn(SignInDto signInDto);
	}
}
