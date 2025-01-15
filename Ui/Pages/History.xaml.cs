using RandomMeal.application.aplications;
using RandomMeal.application.repositories;
using System.Collections.ObjectModel;

namespace RandomMeal.Ui.Pages;

public partial class History : ContentPage
{
    private readonly AppDbContext _dbContext;
    private AplicRecipe aplicRecipe;
    public ObservableCollection<RecipeModel> _recipesHistory = new ObservableCollection<RecipeModel>();

    public History(AppDbContext dbContext)
    {
        InitializeComponent();

        HistoryListView.ItemsSource = _recipesHistory;

        _dbContext = dbContext;

        this.Loaded += GetAplicRecipe;
        this.Loaded += LoadHistory;
    }

    private void GetAplicRecipe(object sender, EventArgs e)
    {
        aplicRecipe = new AplicRecipe(_dbContext, Handler.MauiContext.Services.GetService<IFreeMealApi>());
    }

    private void LoadHistory(object sender, EventArgs e)
    {
        _recipesHistory.Clear();
        
        var recipes = aplicRecipe.OnSelectAll();

        foreach (var recipe in recipes)
        {
            _recipesHistory.Add(recipe);
        }
    }

    private async void HandleSelectRecipe(object sender, EventArgs e)
    {
        try
        {
            if (sender is Button button && button.BindingContext is RecipeModel recipe)
            {
                var response = await aplicRecipe.FindMealById(recipe.Id);

                if (response != null)
                {
                    var meal = response;
                    await Navigation.PushAsync(new Recipe(meal));
                }
            }
        } catch
        {
            await DisplayAlert("Sorry :(", "Failed to load recipe", "OK");
        }
       
    }
}