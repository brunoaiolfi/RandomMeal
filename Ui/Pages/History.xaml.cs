using System.Collections.ObjectModel;

namespace RandomMeal.Ui.Pages;

public partial class History : ContentPage
{
    private IFreeMealApi _api;
    private readonly AppDbContext _dbContext;
    public ObservableCollection<RecipeModel> _recipesHistory = new ObservableCollection<RecipeModel>();

    public History(AppDbContext dbContext)
    {
        InitializeComponent();

        HistoryListView.ItemsSource = _recipesHistory;

        _dbContext = dbContext;

        this.Loaded += GetApi;
        this.Loaded += LoadHistory;
    }

    private void GetApi(object sender, EventArgs e)
    {
        _api = Handler.MauiContext.Services.GetService<IFreeMealApi>();
    }

    private void LoadHistory(object sender, EventArgs e)
    {
        _recipesHistory.Clear();
        
        var recipes = _dbContext.Recipes.ToList();

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
                var response = await _api.LookupMeal(recipe.Id);

                if (response.Meals != null && response.Meals.Any())
                {
                    var meal = response.Meals.First();
                    await Navigation.PushAsync(new Recipe(meal));
                }
            }
        } catch
        {
            await DisplayAlert("Sorry :(", "Failed to load recipe", "OK");
        }
       
    }
}