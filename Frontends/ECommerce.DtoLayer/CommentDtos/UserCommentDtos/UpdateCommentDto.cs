﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.CommentDtos.UserCommentDtos
{
	public class UpdateCommentDto
	{
		public int UserCommentId { get; set; }
		public string NameSurname { get; set; }
		public string? ImageUrl { get; set; }
		public string Email { get; set; }
		public string CommentDetail { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool Status { get; set; }


		//relationship
		public string ProductId { get; set; }
	}
}
