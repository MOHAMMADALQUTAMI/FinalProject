using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Entity;
using FinalProject.DAccess;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


namespace FinalProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly DbAccess _context;
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        public readonly IConfiguration _config;
        public UserController(
            DbAccess context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // create user model
                var user = new User { UserName = model.Username, Email = model.Email };
                // register user
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var claims = new List<Claim>();
                    if (model.IsAdmin)
                    {
                        // add admin claim
                        claims.Add(new Claim(DataClamis.IsAdmin_PolicyName, DataClamis.IsAdmin_ClaimName));
                    }
                    if (model.IsShop)
                    {
                        // add user claim
                        claims.Add(new Claim(DataClamis.IsShop_PolicyName, DataClamis.IsShop_ClaimName));
                    }

                    if (model.IsUser)
                    {
                        // add user claim
                        claims.Add(new Claim(DataClamis.IsUser_PolicyName, DataClamis.IsUser_ClaimName));
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // get user
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user is not null)
                {

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("Login", "Invalid login attempt.");
                        return BadRequest(ModelState);
                    }

                    var claims = await _userManager.GetClaimsAsync(user);
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _config["JwtSettings:Issuer"],
                        audience: _config["JwtSettings:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signingCredentials);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }
            return BadRequest(ModelState);
        }
        /*
        Add Item to basket 
        Update Item to basket 
        Remove Item from basket 
        View Basket 
        View Orders 
        Create Order 
        Register 
        Login
        */
    }
}