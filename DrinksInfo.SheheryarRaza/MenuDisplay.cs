using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksInfo.SheheryarRaza.Models;
using Spectre.Console;

namespace DrinksInfo.SheheryarRaza
{
    public class MenuDisplay
    {
        public void ShowWelcomeMessage()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Drink Menu")
                    .LeftJustified()
                    .Color(Color.Yellow));
            AnsiConsole.MarkupLine("[bold white]Welcome to the Restaurant Drink Menu! :cocktail:[/]");
            AnsiConsole.MarkupLine("[grey]Fetching drink categories from the external API...[/]");
        }

        public void ShowMessage(string message)
        {
            AnsiConsole.MarkupLine(message);
        }

        public void ShowError(string message)
        {
            AnsiConsole.MarkupLine($"[red]ERROR: {message}[/]");
        }

        public void ClearScreen()
        {
            AnsiConsole.Clear();
        }

        public void WaitForUser()
        {
            AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
            Console.ReadKey();
        }

        public void WaitForUserAndClear()
        {
            WaitForUser();
            ClearScreen();
        }

        public void DisplayCategories(List<Category> categories)
        {
            var table = new Table().Title("[bold blue]Drink Categories[/]");
            table.AddColumn("[bold]No.[/]");
            table.AddColumn("[bold]Category[/]");

            for (int i = 0; i < categories.Count; i++)
            {
                table.AddRow($"{i + 1}", categories[i].StrCategory ?? "Unknown");
            }

            AnsiConsole.Write(table);
        }

        public string? GetCategoryChoice(List<Category> categories)
        {
            var prompt = new SelectionPrompt<string>()
                .Title("\nChoose a [green]category[/]:")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(categories.Select(c => c.StrCategory ?? "Unknown"));

            prompt.AddChoice("Quit");

            string? choice = AnsiConsole.Prompt(prompt);

            return choice == "Quit" ? null : choice;
        }

        public void DisplayDrinks(List<DrinkSimple> drinks)
        {
            var table = new Table().Title("[bold blue]Available Drinks[/]");
            table.AddColumn("[bold]No.[/]");
            table.AddColumn("[bold]Drink Name[/]");

            for (int i = 0; i < drinks.Count; i++)
            {
                table.AddRow($"{i + 1}", drinks[i].StrDrink ?? "Unknown");
            }

            AnsiConsole.Write(table);
        }

        public string? GetDrinkChoice(List<DrinkSimple> drinks)
        {
            var prompt = new SelectionPrompt<string>()
                .Title("\nChoose a [green]drink[/]:")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(drinks.Select(d => d.StrDrink ?? "Unknown"));

            prompt.AddChoice("Go Back");

            string? choice = AnsiConsole.Prompt(prompt);

            if (choice == "Go Back")
            {
                return null;
            }

            return drinks.FirstOrDefault(d => d.StrDrink == choice)?.IdDrink;
        }

        public void DisplayDrinkDetail(DrinkDetail drink)
        {
            AnsiConsole.Clear();

            var detailsContent = new Grid()
                .AddColumn()
                .AddColumn();

            detailsContent.AddRow(new Markup("[bold]Category:[/] "), new Markup(drink.StrCategory ?? "Unknown"));
            detailsContent.AddRow(new Markup("[bold]Glass:[/] "), new Markup(drink.StrGlass ?? "Unknown"));
            detailsContent.AddRow(new Markup("[bold]Alcoholic:[/] "), new Markup(drink.StrAlcoholic ?? "Unknown"));

            var drinkDetailsPanel = new Panel(detailsContent)
                .Header($"[bold blue]{drink.StrDrink}[/]")
                .BorderColor(Color.Blue);

            AnsiConsole.Write(drinkDetailsPanel);

            var ingredientsTable = new Table()
                .Title(new TableTitle("[bold]Ingredients[/]"))
                .Border(TableBorder.Square);
            ingredientsTable.AddColumn(new TableColumn("[bold]Measure[/]"));
            ingredientsTable.AddColumn(new TableColumn("[bold]Ingredient[/]"));

            var ingredientsAndMeasures = new List<(string?, string?)>
        {
            (drink.StrIngredient1, drink.StrMeasure1), (drink.StrIngredient2, drink.StrMeasure2),
            (drink.StrIngredient3, drink.StrMeasure3), (drink.StrIngredient4, drink.StrMeasure4),
            (drink.StrIngredient5, drink.StrMeasure5), (drink.StrIngredient6, drink.StrMeasure6),
            (drink.StrIngredient7, drink.StrMeasure7), (drink.StrIngredient8, drink.StrMeasure8),
            (drink.StrIngredient9, drink.StrMeasure9), (drink.StrIngredient10, drink.StrMeasure10),
            (drink.StrIngredient11, drink.StrMeasure11), (drink.StrIngredient12, drink.StrMeasure12),
            (drink.StrIngredient13, drink.StrMeasure13), (drink.StrIngredient14, drink.StrMeasure14),
            (drink.StrIngredient15, drink.StrMeasure15)
        };

            foreach (var (ingredient, measure) in ingredientsAndMeasures)
            {
                if (!string.IsNullOrWhiteSpace(ingredient))
                {
                    ingredientsTable.AddRow(
                        new Markup(measure?.Trim() ?? "[grey]N/A[/]"),
                        new Markup(ingredient?.Trim() ?? "N/A"));
                }
            }

            AnsiConsole.WriteLine();
            AnsiConsole.Write(ingredientsTable);

            if (!string.IsNullOrWhiteSpace(drink.StrInstructions))
            {
                AnsiConsole.WriteLine();
                var instructionsPanel = new Panel(new Markup($"[italic]{drink.StrInstructions}[/]"))
                    .Header("[bold yellow]Instructions[/]")
                    .BorderColor(Color.Yellow);
                AnsiConsole.Write(instructionsPanel);
            }
        }
    }
}
