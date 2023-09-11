using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Interfaces;
using System.IdentityModel.Tokens.Jwt;


namespace FinalProject.Services
{
    public class JwtService : IJwtService
    {
        public string GetUserIdFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == "UserId");

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
            else
            {
                return null; // Return null instead of "false" for better handling
            }
        }
    }
}