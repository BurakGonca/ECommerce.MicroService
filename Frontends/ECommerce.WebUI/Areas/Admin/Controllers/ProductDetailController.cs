using ECommerce.DtoLayer.CatalogDtos.ProductDetailDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/ProductDetail")]
	public class ProductDetailController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductDetailController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		[Route("UpdateProductDetail/{id}")]
		[HttpGet]
		public async Task<IActionResult> UpdateProductDetail(string id)
		{
			
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürün Açıklamaları";
			ViewBag.v3 = "Ürün Açıklama ve Bİlgi Sayfası";
			ViewBag.v0 = "Ürün Açıklama ve Bİlgi İşlemleri";
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7081/api/ProductDetails/ProductDetailsByProductId?id={id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var value = JsonConvert.DeserializeObject<UpdateProductDetailDto>(jsonData);
				if (value is null)
					return RedirectToAction("CreateProductDetail", new { id });
				return View(value);
			}
			return View();
		}
		[Route("UpdateProductDetail/{id}")]
		[HttpPost]
		public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto )
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(updateProductDetailDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7081/api/ProductDetails", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
			}
			return View();
		}






		[Route("CreateProductDetail/{id}")]
		[HttpGet]
		public async Task<IActionResult> CreateProductDetail(string id)
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürün Açıklamaları";
			ViewBag.v3 = "Ürün Açıklama ve Bİlgi Ekleme Sayfası";
			ViewBag.v0 = "Ürün Açıklama ve Bİlgi İşlemleri";

			var model = new CreateProductDetailDto
			{
				ProductId = id
			};

			return View(model);


		}


		[Route("CreateProductDetail/{id}")]
		[HttpPost]
		public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createProductDetailDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7081/api/ProductDetails", stringContent);
			if (responseMessage.IsSuccessStatusCode)
				return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
			return View();
		}
	}
}
