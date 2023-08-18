using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.VModels.SubCategoriesVM;
using FinalProject.VModels;
using FinalProject.VModels.CategoryVM;
using FinalProject.Entity;
using FinalProject.DAccess;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly DbAccess _context;

        public SubCategoryController(DbAccess context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<SubCategoriesVM>> GetSubCategories()
        {
            var subCategory = await _context.SubCategories.Include(s => s.category).ToListAsync();
            return subCategory.Select(s => new SubCategoriesVM
            {
                Id = s.Id,
                Name = s.Name,
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<SubCategoriesVM> Get(int id)
        {
            var SubCategory = await _context.SubCategories.SingleOrDefaultAsync(SubCategories => SubCategories.Id == id);
            return new SubCategoriesVM
            {
                Id = SubCategory.Id,
                Name = SubCategory.Name
            };
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateSubCategoriesVM vM)
        {
            var subcategory = new SubCategory
            {
                
                Name = vM.Name,
              CategoryId=vM.CategoryId
            };
          
           
         _context.SubCategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditSubCategory(int id, CreateSubCategoriesVM vm)
        {
            var Subcategory = await _context.SubCategories.FindAsync(id);
            Subcategory.Name = vm.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var SubCategory = await _context.SubCategories.FindAsync(id);
            _context.Remove(SubCategory);
            //  _dbcontext.SubCategories.Remove(SubCategory);
            await _context.SaveChangesAsync();
        }
    }
}