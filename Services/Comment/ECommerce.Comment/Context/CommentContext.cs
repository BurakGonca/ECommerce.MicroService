using ECommerce.Comment.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Comment.Context
{
	public class CommentContext :DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=localhost,1442;initial Catalog=ECommerceCommentDB;User=sa;Password=Az*123456789");
		}

		public DbSet<Entities.UserComment> UserComments { get; set; }
	}
}
