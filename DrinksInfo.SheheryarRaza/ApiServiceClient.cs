using System.Text.Json;
using DrinksInfo.SheheryarRaza.DTOs;
using DrinksInfo.SheheryarRaza.Models;
using Spectre.Console;

namespace DrinksInfo.SheheryarRaza
{
    public class ApiServiceClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _apiBaseUrl;

        public ApiServiceClient(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }

        public async Task<List<Category>?> GetDrinkCategoriesAsync()
        {
            return await GetApiResponseAsync<CategoryListResponse, List<Category>?>("list.php?c=list", response => response?.Categories);
        }

        public async Task<List<DrinkSimple>?> GetDrinksByCategoryAsync(string category)
        {
            string encodedCategory = Uri.EscapeDataString(category);
            return await GetApiResponseAsync<DrinksListResponse, List<DrinkSimple>?>($"filter.php?c={encodedCategory}", response => response?.Drinks);
        }

        public async Task<DrinkDetail?> GetDrinkDetailsAsync(string drinkId)
        {
            return await GetApiResponseAsync<DrinkDetailResponse, DrinkDetail?>($"lookup.php?i={drinkId}", response => response?.Drinks?.FirstOrDefault());
        }

        private async Task<TResult?> GetApiResponseAsync<TResponse, TResult>(string endpoint, Func<TResponse?, TResult?> selector)
            where TResponse : class
        {
            try
            {
                string url = $"{_apiBaseUrl}{endpoint}";
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                TResponse? apiResponse = JsonSerializer.Deserialize<TResponse>(jsonString, options);

                return selector(apiResponse);
            }
            catch (HttpRequestException e)
            {
                AnsiConsole.MarkupLine($"[red]Request error:[/] {e.Message}");
                return default(TResult);
            }
        }
    }
}
