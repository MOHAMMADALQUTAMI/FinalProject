using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using FinalProject.DAccess;
using Microsoft.EntityFrameworkCore;
using FinalProject.ViewModel;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
            var subCategory = await _context.SubCategories.Include(s => s.Category).ToListAsync();
            return subCategory.Select(s => new SubCategoriesVM
            {
                Id = s.Id,
                Name = s.Name,
                Category = new CategoryVM
                {
                    Id = s.Category.Id,
                    Name = s.Category.Name
                }
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<SubCategoriesVM> Get(int id)
        {
            var SubCategory = await _context.SubCategories.Include(s => s.Category).SingleOrDefaultAsync(SubCategories => SubCategories.Id == id);
            return new SubCategoriesVM
            {
                Id = SubCategory.Id,
                Name = SubCategory.Name,
                Category = new CategoryVM
                {
                    Id = SubCategory.Category.Id,
                    Name = SubCategory.Category.Name
                }
            };
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateSubCategoryVM vM)
        {
            var subcategory = new SubCategory
            {

                Name = vM.Name,
                CategoryId = vM.CategoryId
            };


            _context.SubCategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditSubCategory(int id, CreateSubCategoryVM vm)
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