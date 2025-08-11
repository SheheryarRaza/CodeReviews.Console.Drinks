using DrinksInfo.SheheryarRaza;
using DrinksInfo.SheheryarRaza.Models;

public class Program
{
    private static readonly string ApiBaseUrl = "https://www.thecocktaildb.com/api/json/v1/1/";
    private static readonly ApiServiceClient _apiService = new ApiServiceClient(ApiBaseUrl);
    private static readonly MenuDisplay _display = new MenuDisplay();

    public static async Task Main(string[] args)
    {
        _display.ShowWelcomeMessage();


        while (true)
        {

            List<Category>? categories = await _apiService.GetDrinkCategoriesAsync();

            if (categories == null || categories.Count == 0)
            {
                _display.ShowError("Could not retrieve drink categories. Please check your internet connection.");
                _display.WaitForUser();
                return;
            }

            _display.DisplayCategories(categories);
            string? chosenCategory = _display.GetCategoryChoice(categories);

            if (chosenCategory == null)
            {
                break;
            }

            _display.ClearScreen();
            _display.ShowMessage($"You chose the [bold yellow]'{chosenCategory}'[/] category. Now fetching drinks...");

            List<DrinkSimple>? drinks = await _apiService.GetDrinksByCategoryAsync(chosenCategory);

            if (drinks == null || drinks.Count == 0)
            {
                _display.ShowMessage($"No drinks found in the [bold yellow]'{chosenCategory}'[/] category.");
                _display.WaitForUser();
                continue;
            }

            _display.DisplayDrinks(drinks);
            string? chosenDrinkId = _display.GetDrinkChoice(drinks);

            if (chosenDrinkId == null)
            {
                continue;
            }

            _display.ClearScreen();
            _display.ShowMessage("Fetching drink details...");

            DrinkDetail? drinkDetail = await _apiService.GetDrinkDetailsAsync(chosenDrinkId);

            if (drinkDetail != null)
            {
                _display.DisplayDrinkDetail(drinkDetail);
            }
            else
            {
                _display.ShowError("Could not retrieve drink details.");
            }

            _display.WaitForUserAndClear();
        }
    }
}