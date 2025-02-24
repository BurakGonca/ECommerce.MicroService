﻿using ECommerce.DtoLayer.CatalogDtos.CategoryDtos;
using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/Product")]
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Listesi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7081/api/Product");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpGet]
		[Route("CreateProduct")]
		public async Task<IActionResult> CreateProduct()
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Yeni Ürün Girişi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7081/api/Categories");
			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
			List<SelectListItem> categoryValues = (from c in values
												   select new SelectListItem()
												   {
													   Text=c.CategoryName,
													   Value=c.CategoryID
												   }).ToList();
			ViewBag.CategoryValues = categoryValues;	
			return View();
		}

		[HttpPost]
		[Route("CreateProduct")]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7081/api/Product", stringContent);

			var errorResponse = await responseMessage.Content.ReadAsStringAsync();
			Console.WriteLine(errorResponse);

			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
			return View();
		}

		[Route("DeleteProduct/{id}")]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync($"https://localhost:7081/api/Product?id={id}");
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
			return View();
		}



		[Route("UpdateProduct/{id}")]
		[HttpGet]
		public async Task<IActionResult> UpdateProduct(string id)
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Güncelleme";
			ViewBag.v0 = "Ürün İşlemleri";


			var client1 = _httpClientFactory.CreateClient();
			var responseMessage1 = await client1.GetAsync("https://localhost:7081/api/Categories");
			var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
			var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);
			List<SelectListItem> categoryValues = (from c in values1
												   select new SelectListItem()
												   {
													   Text = c.CategoryName,
													   Value = c.CategoryID
												   }).ToList();
			ViewBag.CategoryValues = categoryValues;


			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7081/api/Product/{id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
				return View(values);
			}
			return View();
		}


		[Route("UpdateProduct/{id}")]
		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{


			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7081/api/Product/", stringContent);
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
			return View();
		}




		[Route("ProductListWithCategory")]
		public async Task<IActionResult> ProductListWithCategory()
		{
			ViewBag.v1 = "Anasayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Listesi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7081/api/Product/ProductListWithCategory");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
				return View(values);
			}
			return View();
		}


	}
}
