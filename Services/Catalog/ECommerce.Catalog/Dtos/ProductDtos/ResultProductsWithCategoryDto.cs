using ECommerce.Catalog.Dtos.CategoryDtos;
using ECommerce.Catalog.Entities;

namespace ECommerce.Catalog.Dtos.ProductDtos
{
	public class ResultProductsWithCategoryDto
	{
		public string ProductID { get; set; }
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public string ProductImageUrl { get; set; }
		public string ProductDescription { get; set; }


		//relationships
		public string CategoryId { get; set; }
		public ResultCategoryDto Category { get; set; }

	}
}
