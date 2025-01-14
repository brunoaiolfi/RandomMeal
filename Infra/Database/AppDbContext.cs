using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<RecipeModel> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RandomMeal.db");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}

