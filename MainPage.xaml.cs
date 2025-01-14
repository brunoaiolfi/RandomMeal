using Microsoft.EntityFrameworkCore;
using RandomMeal.Ui.Pages;

namespace RandomMeal
{
    public partial class MainPage : ContentPage
    {
        private IFreeMealApi _api;
        private readonly AppDbContext _dbContext;

        public MainPage(AppDbContext dbContext)
        {
            InitializeComponent();
            this.Loaded += getApi;

            _dbContext = dbContext;
        }

        private void getApi(object sender, EventArgs e)
        {
            _api = Handler.MauiContext.Services.GetService<IFreeMealApi>();
        }

        private async void HandleFindNewMeal(object sender, EventArgs e)
        {
            try
            {
                var meal = await _api.GetRandomMeal();

                _dbContext.Add(new RecipeModel()
                {
                    Name = meal.Meals[0].StrMeal,
                    Id = meal.Meals[0].IdMeal,
                    CreatedAt = new DateTime()
                });

                await _dbContext.SaveChangesAsync();

                await Navigation.PushAsync(new Recipe(meal.Meals[0]));
            }
            catch (Exception error)
            {
                DisplayAlert("Sorry :( ", $"An error has ocurred, try again later. {error.Message}", "OK");
            }
        }

        private void HandleNavigateToHistory(object sender, EventArgs e) {
            Navigation.PushAsync(new History(_dbContext));
        }
    }

}
