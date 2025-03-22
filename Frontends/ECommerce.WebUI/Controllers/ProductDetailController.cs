﻿using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using ECommerce.DtoLayer.CommentDtos.UserCommentDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
	public class ProductDetailController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;

		public ProductDetailController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public IActionResult Index(string id)
		{

			ViewBag.Id = id;
			return View();
		}


		[HttpGet]
		public async Task<PartialViewResult> AddComment(string id)
		{
			ViewBag.Id = id;
			return PartialView();
		}

		[HttpPost]
		public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
		{
			createCommentDto.ImageUrl = "data:image/jpeg;base64,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";

			//imageUrl login olduktan sonra user'dan gelecek hatta nameSurname de oradan gelecek.

			string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var jsonData = JsonConvert.SerializeObject(createCommentDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7141/api/Comments", stringContent);
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("Index", "ProductDetail", new { id = createCommentDto.ProductId });

			return View();

			
		}


	}
}
