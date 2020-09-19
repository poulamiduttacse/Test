
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Repositories;

namespace TechEvent_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepositories _categoriesRepositories;

        public CategoriesController(ICategoriesRepositories categoriesRepositories)
        {
            _categoriesRepositories = categoriesRepositories;
        }

   
        [HttpGet]
        public async Task<IActionResult> GetCategoryAsync()
        {
            var result = await _categoriesRepositories.GetCategoryAsync();

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int id)
        {
            Category result;
            if (id > 0)
                result = await _categoriesRepositories.FindAsync(id);
            else
                return BadRequest();
            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                await _categoriesRepositories.UpdateCategoryAsync(id, category);
            }
            catch
            {
                return NoContent();
            }
            return Ok();
        }

        
        [HttpPost]
        public async Task<IActionResult> PostCategoryAsync(Category category)
        {
            if (category == null || string.IsNullOrWhiteSpace(category.CategoryName))
                return BadRequest();
            await _categoriesRepositories.InsertCategoryAsync(category);
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                if (id > 0)
                    await _categoriesRepositories.DeleteCategoryAsync(id);
                else
                    return BadRequest();
            }
            catch { return NotFound(); }

            return Ok();
        }


    }
}
