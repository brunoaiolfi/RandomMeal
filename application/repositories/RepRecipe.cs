using Microsoft.EntityFrameworkCore;
using RandomMeal.application.repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomMeal.application.repositories
{
    class RepRecipe : IRepoBase<RecipeModel>
    {
        private AppDbContext DbContext { get; }

        public RepRecipe(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }


        public List<RecipeModel> SelectAll()
        {
           return DbContext.Recipes.ToList();
        }

        public async Task<RecipeModel> Create(RecipeModel dto)
        {
            var Response = DbContext.Add(new RecipeModel()
            {
                Name = dto.Name,
                Id = dto.Id,
                CreatedAt = dto.CreatedAt
            });

            await DbContext.SaveChangesAsync();

            return Response.Entity;
        }
    }
}
