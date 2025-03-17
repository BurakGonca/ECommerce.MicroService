using AutoMapper;
using ECommerce.Comment.Context;
using ECommerce.Comment.Dtos;
using ECommerce.Comment.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Comment.Services
{
	public class CommentService : ICommentService
	{
		private readonly CommentContext _context;
		private readonly IMapper _mapper;

		public CommentService(CommentContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<ResultCommentDto>> GetAllCommentAsync()
		{
			var values = await _context.UserComments.ToListAsync();
			var comments = _mapper.Map<List<ResultCommentDto>>(values);
			return comments;
		}

		public async Task<GetByIdCommentDto> GetByIdCommentAsync(int commentId)
		{
			var value = await _context.UserComments.Where(c => c.UserCommentId == commentId).FirstOrDefaultAsync();
			return _mapper.Map<GetByIdCommentDto>(value);
		}

		public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
		{
			var value = _mapper.Map<UserComment>(createCommentDto);
			await _context.UserComments.AddAsync(value);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
		{
			var value = _mapper.Map<UserComment>(updateCommentDto);
			_context.UserComments.Update(value);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCommentAsync(int commentId)
		{
			var value = await _context.UserComments.Where(c => c.UserCommentId == commentId).FirstOrDefaultAsync();
			_context.UserComments.Remove(value);
			await _context.SaveChangesAsync();
		}




	}
}
