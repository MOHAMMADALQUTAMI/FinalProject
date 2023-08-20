using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAccess;
using FinalProject.VModels;
using FinalProject.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace FinalProject.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ShopController : ControllerBase
  {
    private readonly DbAccess _context;
    public readonly UserManager<User> _userManager;
    public readonly SignInManager<User> _signInManager;
    public readonly IConfiguration _config;
    public ShopController(DbAccess context,
         UserManager<User> userManager,
         SignInManager<User> signInManager,
         IConfiguration config)
    {
      _context = context;
      _userManager = userManager;
      _signInManager = signInManager;
      _config = config;
    }


    /*
    View Orders 
    View Foods
    Add Food
    Edit Food
    Edit order 
    Register 
    Login

    */
    // View Foods
    [HttpGet]
    [Authorize(Policy = DataClamis.AddFoodPolicyName)]
    public async Task<ActionResult<IEnumerable<FoodVM>>> GetFood()
    {
      var foods = await _context.Foods.Include(p => p.Owner)
      .ToListAsync();
      return foods.Select(e => new FoodVM
      {
        Name = e.Name,
        Price = e.Price,
        Description = e.Description,
        OwnerId = e.Owner.Id

      }).ToList();

    }
    // View Order 
    [HttpGet]
    [Authorize(Policy = DataClamis.AddOrderPolicyName)]
    public async Task<ActionResult<IEnumerable<OrderVM>>> GetOrder()
    {
      var order = await _context.Orders.Include(p => p.Basket)
      .ToListAsync();
      return order.Select(e => new OrderVM
      {
        Phone = e.Phone,
        TotalPrice = e.TotalPrice,
        CurrentStatus = e.CurrentStatus,

      }).ToList();

    }
    // add Foods
    [HttpPost]
    [Authorize(Policy = DataClamis.UsersPolicyName)]
    public async Task<ActionResult> AddFood(FoodVM model)
    {
      var foods = new Food
      {


        OwnerId = model.Owner.Id,
        Name = model.Name,
        Price = model.Price,
        Description = model.Description

      };
      _context.Foods.Add(foods);
      await _context.SaveChangesAsync();
      return Ok();
    }
    // EditFoods
    [HttpPut]
    [Authorize(Policy = DataClamis.UsersPolicyName)]
    public async Task<ActionResult> UpdateFood(FoodVM model)
    {
      var foods = await _context.Foods.FindAsync(model.Id);
      if (foods != null)
      {
        foods.Id = model.Id;
        foods.Name = model.Name;
        foods.Price = model.Price;
        foods.Description = model.Description;
        await _context.SaveChangesAsync();
        return Ok();
      }
      else
      {
        return NoContent();
      }
    }
    // Edit Order
    [HttpPut]
    [Authorize(Policy = DataClamis.UsersPolicyName)]
    public async Task<ActionResult> EditOrder(OrderVM model)
    {
      var order = await _context.Orders.FindAsync(model.Id);
      if (order != null)
      {
        order.Id = model.Id;
        order.Phone = model.Phone;
        order.TotalPrice = model.TotalPrice;
        await _context.SaveChangesAsync();
        return Ok();
      }
      else
      {
        return NoContent();
      }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Register(RegisterUser model)
    {
      {
        if (ModelState.IsValid)
        {
          var user = new User { UserName = model.UserName, Email = model.Email }; //creat user model
          var result = await _userManager.CreateAsync(user, model.Password);         //register user

          if (result.Succeeded)
          {
            var claims = new List<Claim>();
            if (model.IsAdmin)
            {
              claims.Add(new Claim(DataClamis.AdminClaimName, DataClamis.AdminClaimName));
            }
            else
            {
              claims.Add(new Claim(DataClamis.UsersClaimName, DataClamis.UsersClaimName));

            }
            await _userManager.AddClaimsAsync(user, claims);
            return Ok();
          }
          else
          {
            foreach (var error in result.Errors)
            {
              ModelState.AddModelError(string.Empty, error.Description);
            }
          }
        }
        return BadRequest(ModelState);
      }
    }

  
       [HttpPost(Name = "Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUser model)
        {
            if(ModelState.IsValid)
            {
                //get user
             var user = await _userManager.FindByNameAsync(model.UserName);
             if (user is not null)
             {
                 var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Login", "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
               
             }
             var claims =await _userManager.GetClaimsAsync(user);
             var Securitykey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
              var signingCredentials = new SigningCredentials(Securitykey, SecurityAlgorithms.HmacSha256);
              var Token =new JwtSecurityToken(
                issuer:_config["JwtSettings:Issuer"],
                audience:_config["JwtSettings:Audiience"],
                claims:claims,
                expires:DateTime.UtcNow.AddMinutes(30),
                signingCredentials:signingCredentials);
              return Ok(new {Token=new JwtSecurityTokenHandler().WriteToken(Token)});
            }
            return BadRequest(ModelState);
        }
}
}
