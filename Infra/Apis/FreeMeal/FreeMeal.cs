using Refit;

public interface IFreeMealApi
{
    [Get("/random.php")]
    Task<MealResponseDTO> GetRandomMeal();

    [Get("/lookup.php")]
    Task<MealResponseDTO> LookupMeal([Query] string i);
}