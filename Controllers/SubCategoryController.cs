using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.ViewModel;
using FinalProject.Models;
using FinalProject.Entity;
using FinalProject.ViewModel.CategoryVM;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using FinalProject.ViewModel.SubCategoryVM;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly Entity.DbContext _dbcontext;
        public SubCategoryController(Entity.DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IEnumerable<SubCategoryVM>> GetSubCategory()
        {
            var subCategory = await _dbcontext.subcategories.Include(s => s.Category).ToListAsync();
            return subCategory.Select(s => new SubCategoryVM
            {
                Id = s.Id,
                Name = s.Name,
                Category = new CategoryVM
                {
                    Id = s.Id,
                    Name = s.Name
                }


            }).ToList();
        }
          [HttpGet("{id}")]
    public async Task<SubCategoryVM> Get(int id)
    {
        var SubCategory = await _dbcontext.subcategories.SingleOrDefaultAsync(SubCategories => SubCategories.Id == id);
        return new SubCategoryVM
        {
            Id = SubCategory.Id,
            Name = SubCategory.Name
        };
    }
      [HttpPost]
        public async Task<IActionResult> Post(CreateSubCategoryVM vM)
        {
            var subcategory = new SubCategory
            {
                name = vM.Name,
                CategoryID = vM.CategoryId,




            };
            _dbcontext.subcategories.Add(subcategory);
            await _dbcontext.SaveChangesAsync();
            return Ok();

        }
        
          [HttpPut]
    public async Task<IActionResult> EditSubCategory(int id, CreateSubCategoryVM vm)
    {
        var Subcategory = await _dbcontext.subcategories.FindAsync(id);
        Subcategory.Name = vm.Name;
        await _dbcontext.SaveChangesAsync();
        return Ok();
    }
      [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var SubCategory  = await _dbcontext.subcategories.FindAsync(id);
        _dbcontext.Remove(SubCategory); 
      //  _dbcontext.subcategories.Remove(SubCategory);
        await _dbcontext.SaveChangesAsync();
    }
    }
}