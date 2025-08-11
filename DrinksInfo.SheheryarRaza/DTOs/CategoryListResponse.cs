using System.Text.Json.Serialization;
using DrinksInfo.SheheryarRaza.Models;

namespace DrinksInfo.SheheryarRaza.DTOs
{
    public class CategoryListResponse
    {
        [JsonPropertyName("drinks")]
        public List<Category>? Categories { get; set; }
    }
}
