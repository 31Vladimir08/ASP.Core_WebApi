namespace GatewayWebApi.ModelsDto
{
    public class ProductDto
    {
        public string? ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public int? UnitsOnOrder { get; set; }

        public int? ReorderLevel { get; set; }

        public bool? Discontinued { get; set; }

        public string? CategoryId { get; set; }
    }
}
