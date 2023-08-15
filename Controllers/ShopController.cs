using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using FinalProject.VModels;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    public class ShopController :ControllerBase
    {
        private readonly DbContext _dbcontext;
        public ShopController(DbContext dbcontext)
        {
        _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>>GetShop()
        {
            return await _dbcontext.Shops.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>>GetById(int id)
        {
            var shops = await _dbcontext.Shops.Where(x => x.Id == id).ToListAsync();
            if(shops ==null ||shops.Count ==0)
            {
                return NotFound();
            }
            return shops;
        }
        [HttpPost]
        public async Task<ActionResult>AddShop(Shop model)
        {
         var shops =new Shop();
            shops.Id=model.Id;
            shops.Name=model.Name;
            shops.Email=model.Email;
            
            _dbcontext.Add(shops);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }


        

    }    
}     