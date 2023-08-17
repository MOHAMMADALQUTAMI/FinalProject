using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace FinalProject.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FoodController :ControllerBase
    {
        private readonly DbContext _dbcontext;
        public FoodController(DbContext dbcontext)
        {
        _dbcontext = dbcontext;
        }
    }
}