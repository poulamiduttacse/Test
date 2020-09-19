using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
   public interface ICategoriesRepositories
    {
        Task<IList<Category>> GetCategoryAsync();
        Task<Category> FindAsync(int id);
        Task UpdateCategoryAsync(int id, Category category);
        Task InsertCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);

    }
}
