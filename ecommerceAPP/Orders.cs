using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace ecommerceApp
{
    public class Orders
    {
        [Key]
        [IsRetrievable(true)]
        public string OrderItemID { get; set; }

        [IsSearchable, IsFilterable, IsRetrievable(true)]
        public string CustomerName { get; set; }

        [IsSearchable, IsRetrievable(true)]
        public string CustomerEmail { get; set; }

        [IsSearchable, IsFilterable, IsRetrievable(true)]
        public string ProductName { get; set; }

        [IsRetrievable(true)]
        public string ImageURL { get; set; }

        [IsRetrievable(true)]
        public string Description { get; set; }

        [IsSearchable, IsFilterable, IsRetrievable(true)]
        public string CategoryName { get; set; }

        [IsRetrievable(true)]
        public long Quantity { get; set; }

        [IsSearchable, IsFilterable, IsRetrievable(true)]
        public string OrderStatus { get; set; }

        [IsRetrievable(true)]
        public double Price { get; set; }
    }
}
