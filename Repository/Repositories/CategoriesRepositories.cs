using Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoriesRepositories : ICategoriesRepositories
    {
        private TecheventContext _context { get; set; }
        public CategoriesRepositories(TecheventContext context)
        {
            _context = context;
        }

        public async Task<IList<Category>> GetCategoryAsync()
        {
            return _context.Category.ToList();
        }

        public async Task<Category> FindAsync(int id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return null;
            }

            return category;
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            try
            {
                _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    throw new ApplicationException("Not found");
                }
                else
                {
                    throw;
                }
            }
        }
        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }

        public async Task InsertCategoryAsync (Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                throw new ApplicationException("Not found");
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
