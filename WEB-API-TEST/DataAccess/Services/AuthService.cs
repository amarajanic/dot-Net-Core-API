using DataAccess.DbContext;
using DataAccess.DbModel;
using DataAccess.Helpers;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Services
{
    public class AuthService : IAuthService
    {
        private readonly TestDbContext _context;
        PasswordHelper passwordHelper = new PasswordHelper();
        JWTGenerator jwtGenerator;
        public AuthService(TestDbContext context)
        {
            _context = context;
            jwtGenerator = new JWTGenerator(context);
        }
        public async Task<string> Register(UserRegister request)
        {
            if(request.Password == request.ConfirmPassword)
            {
               if (_context.Users.Where(x=>x.Username == request.Username).Any())
                {
                    return "User with that username already exists!";
                }
               passwordHelper.CreatePasswordHash(request.Password, out byte[] passwordhash, out byte[] passwordSalt);
               var dbUser = new DbUser()
                 {
                   FirstName = request.FirstName,
                   LastName = request.LastName,
                   Username = request.Username,
                   PasswordHash = passwordhash,
                   PasswordSalt = passwordSalt,
                   RoleId = 3
                 };

                await _context.AddAsync(dbUser);
                await _context.SaveChangesAsync();
                return "User registrated successfully!";
            }
            else
            {
                return "Passwords don't match!";
            }
        }

        public async Task<LoginResponse> Login(UserLogin request)
        {
            var user = await _context.Users.Where(x => x.Username == request.Username).FirstOrDefaultAsync();

            if(user == null)
            {
                throw new Exception("Username or password doesn't exist.");
            }
            if (!passwordHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Username or password doesn't exist.");
            }

            string token = await jwtGenerator.CreateToken(user);

            var refreshToken = jwtGenerator.GenerateRefreshToken();
            jwtGenerator.SetRefreshToken(refreshToken, user);

            var loginResponse = new LoginResponse() { AccessToken = token , RefreshToken = refreshToken.Token};

            return loginResponse;

        }

        public async Task<LoginResponse> Refresh(string refresh)
        {
            var userRefreshToken = await _context.Users.Where(x=>x.RefreshToken == refresh).FirstOrDefaultAsync();

            if (userRefreshToken == null || userRefreshToken.RefreshToken != refresh)
            {
                throw new Exception("Invalid refresh token!");
            }
            if (userRefreshToken.TokenExpires >= DateTime.Now)
            {
                throw new Exception("Refresh token expired!");
            }

            string token = await jwtGenerator.CreateToken(userRefreshToken);

            var refreshToken = jwtGenerator.GenerateRefreshToken();
            jwtGenerator.SetRefreshToken(refreshToken, userRefreshToken);

            var refreshResponse = new LoginResponse() { AccessToken = token, RefreshToken = refreshToken.Token };

            return refreshResponse;

        }
    }
}
