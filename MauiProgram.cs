using Microsoft.Extensions.Logging;
using Refit;
using Microsoft.Extensions.DependencyInjection;

namespace RandomMeal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IFreeMealApi>(RestService.For<IFreeMealApi>("http://www.themealdb.com/api/json/v1/1"));
            builder.Services.AddDbContext<AppDbContext>();

            // Criar o banco de dados durante a inicialização
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
