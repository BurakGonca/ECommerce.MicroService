using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.CatalogDtos.ProductImageDtos
{
	public class UpdateProductImageDto
	{
		public string ProductImageId { get; set; }
		public string Image1 { get; set; }
		public string Image2 { get; set; }
		public string Image3 { get; set; }

		//relationships
		public string ProductId { get; set; }
	}
}
