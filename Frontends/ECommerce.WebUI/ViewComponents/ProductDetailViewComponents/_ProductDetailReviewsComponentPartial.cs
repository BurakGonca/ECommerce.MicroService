using ECommerce.DtoLayer.CommentDtos.UserCommentDtos;
using ECommerce.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.ProductDetailViewComponents
{
	public class _ProductDetailReviewsComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _ProductDetailReviewsComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		
		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			ViewBag.Id = id;
			//string token = await TokenHelper.GetAccessTokenAsync();

			var client = _httpClientFactory.CreateClient();

			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var responseMessage = await client.GetAsync($"http://localhost:7075/api/Comments/CommentListByProductId?id={id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
				return View(values);
			}

			return View();
		}
	}
}
