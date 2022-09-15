using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }

        [BsonElement("name")]
        public string? ProductName { get; set; }

        [BsonElement("quantityPerUnit")]
        public string? QuantityPerUnit { get; set; }

        [BsonElement("unitPrice")]
        public decimal? UnitPrice { get; set; }

        [BsonElement("unitsInStock")]
        public int? UnitsInStock { get; set; }

        [BsonElement("unitsOnOrder")]
        public int? UnitsOnOrder { get; set; }

        [BsonElement("reorderLevel")]
        public int? ReorderLevel { get; set; }

        [BsonElement("discontinued")]
        public bool? Discontinued { get; set; }

        [BsonElement("categoryId")]
        public string? CategoryId { get; set; }
    }
}
