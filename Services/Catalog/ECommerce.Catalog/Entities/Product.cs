using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce.Catalog.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductID { get; set; }

        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }


		//relationships
		[BsonRepresentation(BsonType.ObjectId)]
		public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }



    }
}
