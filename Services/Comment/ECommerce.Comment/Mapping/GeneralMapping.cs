using AutoMapper;
using ECommerce.Comment.Dtos;
using ECommerce.Comment.Entities;

namespace ECommerce.Comment.Mapping
{
	public class GeneralMapping :Profile
	{
        public GeneralMapping()
        {
			CreateMap<UserComment, ResultCommentDto>().ReverseMap();
			CreateMap<UserComment, CreateCommentDto>().ReverseMap();
			CreateMap<UserComment, UpdateCommentDto>().ReverseMap();
			CreateMap<UserComment, GetByIdCommentDto>().ReverseMap();
		}
    }
}
