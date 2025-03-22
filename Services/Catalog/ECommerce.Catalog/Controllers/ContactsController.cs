using ECommerce.Catalog.Dtos.ContactDtos;
using ECommerce.Catalog.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Catalog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController : ControllerBase
	{
		private readonly IContactService _contactService;

		public ContactsController(IContactService categoryService)
		{
			_contactService = categoryService;
		}


		[HttpGet]
		public async Task<IActionResult> ContactList()
		{
			var values = await _contactService.GetAllContactAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetContactById(string id)
		{
			var value = await _contactService.GetByIdContactAsync(id);
			return Ok(value);
		}


		[HttpPost]
		public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
		{
			await _contactService.CreateContactAsync(createContactDto);
			return Ok("Mesaj başarıyla eklendi");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteContact(string id)
		{
			await _contactService.DeleteContactAsync(id);
			return Ok("Mesaj başarıyla silindi");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
		{
			await _contactService.UpdateContactAsync(updateContactDto);
			return Ok("Mesaj başarıyla güncellendi");
		}
	}
}
