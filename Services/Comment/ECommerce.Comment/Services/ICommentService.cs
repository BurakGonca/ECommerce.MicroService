using ECommerce.Comment.Dtos;

namespace ECommerce.Comment.Services
{
	public interface ICommentService
	{
		Task<List<ResultCommentDto>> GetAllCommentAsync();
		Task CreateCommentAsync(CreateCommentDto createCommentDto);
		Task UpdateCommentAsync(UpdateCommentDto updateCommentDto);
		Task DeleteCommentAsync(int commentId);
		Task<GetByIdCommentDto> GetByIdCommentAsync(int commentId);
	}
}
