﻿namespace GatewayWebApi.ModelsDto.Filters
{
    public class ProductFilter
    {
        public string? ProductName { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public int? UnitsOnOrder { get; set; }

        public int? ReorderLevel { get; set; }

        public bool? Discontinued { get; set; }

        public string? CategoryName { get; set; }
    }
}
