using DataAccess.DbContext;
using DataAccess.DbModel;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DataAccess.Helpers
{
    public class JWTGenerator
    {
        private readonly TestDbContext _context;
        public JWTGenerator(TestDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateToken(DbUser user)
        {
            var role = await _context.Roles.Where(x => x.Id == user.RoleId).FirstOrDefaultAsync();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role.Name),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("TOP_SECRET_KEY_VALUE"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
            };

            return refreshToken;
        }

        public void SetRefreshToken(RefreshToken newRefreshToken, DbUser user)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };

            //Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions); //not working Response
            //refreshToken needs to be saved in client header
            

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            _context.Users.Update(user);
            _context.SaveChanges();

        }
    }
}
