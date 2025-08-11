using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DrinksInfo.SheheryarRaza.Models;

namespace DrinksInfo.SheheryarRaza.DTOs
{
    public class CategoryListResponse
    {
        [JsonPropertyName("drinks")]
        public List<Category>? Categories { get; set; }
    }
}
