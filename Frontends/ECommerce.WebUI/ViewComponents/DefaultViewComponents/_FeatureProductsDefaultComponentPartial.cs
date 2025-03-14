﻿using ECommerce.DtoLayer.CatalogDtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.ViewComponents.DefaultViewComponents
{
	public class _FeatureProductsDefaultComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _FeatureProductsDefaultComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		
		/// <summary>
		/// Son eklenen 8 ürüne göre filtreleyerek getiriyor
		/// </summary>
		/// <returns></returns>
		public async Task<IViewComponentResult> InvokeAsync()
		{

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7081/api/Product/ProductListWithCategory");


			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData).TakeLast(8).ToList();
				
				return View(values);
			}
			return View();
		}

		


	}
}
