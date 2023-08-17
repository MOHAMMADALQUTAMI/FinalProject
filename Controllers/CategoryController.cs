using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using FinalProject.ViewModel.CategoryVM;
using FinalProject.ViewModel;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly Entity.DbContext _dbcontext;
        public CategoryController(Entity.DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryVM>> GetCategory()
        {
            
            var category = await _dbcontext.categorys.ToListAsync();
            return category.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }
        [HttpGet("{id}")]
        public async Task<CategoryVM> GetCategoryById(int id)
        {
            var category = await _dbcontext.categorys.FindAsync(id);
            return new CategoryVM
            {
                Id = category.Id,
                Name = category.Name
            };

        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryVM vM)
        {
            var category = new Category
            {
                Name = vM.Name,
            };
            await _dbcontext.AddAsync(category);
            await _dbcontext.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
         //   var Category = await _dbcontext.categorys.FindAsync(id);
           // _dbcontext.categorys.Remove(Category);
           
            var entity = await _dbcontext.categorys.FindAsync(id);
            _dbcontext.Remove(entity); 
            await _dbcontext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            
            var category = await _dbcontext.categorys.FindAsync(id);
            category.Name = category.Name;
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }

    }
}