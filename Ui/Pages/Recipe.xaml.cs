using System.Collections.ObjectModel;

namespace RandomMeal.Ui.Pages;

public partial class Recipe : ContentPage
{
	ObservableCollection<Ingredient> _ingredients = new ObservableCollection<Ingredient>();

    public Recipe(MealDTO recipe)
	{
		InitializeComponent();

		MealTitle.Text = recipe.StrMeal;
        Instructions.Text = recipe.StrInstructions;
		RecipeImage.Source = recipe.StrMealThumb;
        IngredientsListView.ItemsSource = _ingredients;

        foreach (var ingredient in recipe.GetIngredients())
		{
            _ingredients.Add(ingredient);
        }
    }

}