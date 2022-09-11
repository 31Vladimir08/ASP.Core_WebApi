using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductCategoryApi.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }

        [BsonElement("name")]
        public string? CategoryName { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }
    }
}
