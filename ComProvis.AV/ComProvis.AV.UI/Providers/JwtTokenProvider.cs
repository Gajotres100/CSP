using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComProvis.AV.UI.Providers
{
    public class JwtTokenProvider
    {
        public JwtSecurityToken GenerateJwtToken(string userName, string role)
        {
            var claimdata = new[] { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, role) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kjaksfjashfjashfjashjfashu"));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(                
                expires: DateTime.Now.AddDays(1),
                claims: claimdata,
                signingCredentials: signInCredentials
                );

            return token;

        }
    }
}
