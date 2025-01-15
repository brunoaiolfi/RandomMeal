using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomMeal.application.repositories.interfaces
{
    public interface IRepoBase<Model>
    {
        Task<Model> Create(Model obj);
        List<Model> SelectAll();
    }
}
