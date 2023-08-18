using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.DAccess;
using FinalProject.VModels.CategoryVM;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
         private readonly DbAccess _context;

        public CategoryController(DbAccess context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryVM>> GetCategories()
        {
            
            var category = await _context.categorys.ToListAsync();
            return category.Select(c => new CategoryVM
            {
               
                Name = c.Name,
            }).ToList();
        }
        [HttpGet("{id}")]
        public async Task<CategoryVM> GetCategoryById(int id)
        {
            var category = await _context.categorys.FindAsync(id);
            return new CategoryVM
            {
             
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
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
         //   var Category = await _dbcontext.categorys.FindAsync(id);
           // _dbcontext.categorys.Remove(Category);
           
            var entity = await _context.categorys.FindAsync(id);
            _context.Remove(entity); 
            await _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            
            var category = await _context.categorys.FindAsync(id);
            category.Name = category.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}