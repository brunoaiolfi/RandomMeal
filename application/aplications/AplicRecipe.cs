using RandomMeal.application.repositories;

namespace RandomMeal.application.aplications
{
    internal class AplicRecipe
    {
        private RepRecipe repository;
        private IFreeMealApi _api;

        public AplicRecipe(AppDbContext dbContext, IFreeMealApi api)
        {
            repository = new RepRecipe(dbContext);
            _api = api;
        }

        public async Task<MealResponseDTO> OnRequestRandomMeal()
        {
            var meal = await _api.GetRandomMeal();

            if (meal.Meals[0].IdMeal != null)
            {
                await repository.Create(
                    new RecipeModel()
                    {
                        Name = meal.Meals[0].StrMeal,
                        Id = meal.Meals[0].IdMeal,
                        CreatedAt = new DateTime()
                    }
                );
            }

            return meal;
        }

        public async Task<MealDTO> FindMealById(string Id)
        {
            var meals = await _api.LookupMeal(Id);
            return meals.Meals[0];
        }

        public List<RecipeModel> OnSelectAll()
        {
            return repository.SelectAll();
        }
    }
}
