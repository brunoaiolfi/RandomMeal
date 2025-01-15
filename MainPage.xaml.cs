using Microsoft.EntityFrameworkCore;
using RandomMeal.application.aplications;
using RandomMeal.application.repositories;
using RandomMeal.Ui.Pages;

namespace RandomMeal
{
    public partial class MainPage : ContentPage
    {
        private AppDbContext _dbContext;
        private AplicRecipe aplicRecipe;

        public MainPage(AppDbContext dbContext)
        {
            InitializeComponent();
            this.Loaded += getAplicRecipe;
            _dbContext = dbContext;
        }

        private void getAplicRecipe(object sender, EventArgs e)
        {
            aplicRecipe = new AplicRecipe(_dbContext, Handler.MauiContext.Services.GetService<IFreeMealApi>());
        }

        private async void HandleFindNewMeal(object sender, EventArgs e)
        {
            try
            {
                var meal = await aplicRecipe.OnRequestRandomMeal();
                await Navigation.PushAsync(new Recipe(meal.Meals[0]));
            }
            catch (Exception error)
            {
                DisplayAlert("Sorry :( ", $"An error has ocurred, try again later. {error.Message}", "OK");
            }
        }

        private void HandleNavigateToHistory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new History(_dbContext));
        }
    }

}
