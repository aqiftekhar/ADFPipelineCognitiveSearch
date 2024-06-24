using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ecommerceApp
{
    public class CognitiveSearchService
    {
        private readonly ISearchIndexClient _indexClient;

        public CognitiveSearchService(string searchServiceName, string apiKey, string indexName)
        {
            var searchCredentials = new SearchCredentials(apiKey);
            _indexClient = new SearchIndexClient(searchServiceName, indexName, searchCredentials);
        }

        public async Task<IEnumerable<Orders>> SearchProductsAsync(string query)
        {
            try
            {
                var parameters = new SearchParameters
                {
                    Select = new[] { "*" }, 
                    SearchMode = SearchMode.All,
                    QueryType = QueryType.Full
                };

                // Check if the query contains a filter
                var filterIndex = query.IndexOf("$filter=");
                if (filterIndex != -1)
                {
                    var filter = query.Substring(filterIndex + 8);
                    query = query.Substring(0, filterIndex);
                    parameters.Filter = filter;
                }

                var results = await _indexClient.Documents.SearchAsync(query, parameters);
                var orders = results.Results.Select(result => 
                {
                    var order = new Orders
                    {
                        OrderItemID = (string)result.Document["OrderItemID"],
                        CustomerName = (string)result.Document["CustomerName"],
                        CustomerEmail = (string)result.Document["CustomerEmail"],
                        ProductName = (string)result.Document["ProductName"],
                        ImageURL = (string)result.Document["ImageURL"],
                        Description = (string)result.Document["Description"],
                        CategoryName = (string)result.Document["CategoryName"],
                        Quantity = (long)result.Document["Quantity"],
                        OrderStatus = (string)result.Document["OrderStatus"],
                        Price = double.TryParse((string)result.Document["Price"], out double price)? price : 0,
                    };
                    return order;
                });

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Searched data not found or please check your credentials: {ex.Message}");
            }
        }
    }
}