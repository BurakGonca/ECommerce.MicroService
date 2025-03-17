using ECommerce.Comment.Context;
using ECommerce.Comment.Dtos;
using ECommerce.Comment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Comment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
	{
		private readonly ICommentService _commentService;

		public CommentsController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet]
		public async Task<IActionResult> CommentList()
		{
			var values = await _commentService.GetAllCommentAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCommentById(int id)
		{
			var value = await _commentService.GetByIdCommentAsync(id);
			return Ok(value);
		}


		[HttpPost]
		public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
		{
			await _commentService.CreateCommentAsync(createCommentDto);
			return Ok("Yorum başarıyla eklendi.");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
		{
			await _commentService.UpdateCommentAsync(updateCommentDto);
			return Ok("Yorum başarıyla güncellendi.");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteComment(int id)
		{
			await _commentService.DeleteCommentAsync(id);
			return Ok("Yorum başarıyla silindi.");
		}




	}
}
