using DataAccess.DbContext;
using DataAccess.DbModel;
using DataAccess.Helpers;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> Register(UserRegister request)
        {
            if(request.Password == request.ConfirmPassword)
            {
                passwordHelper.CreatePasswordHash(request.Password, out byte[] passwordhash, out byte[] passwordSalt);
                var dbUser = new DbUser()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username,
                    PasswordHash = passwordhash,
                    PasswordSalt = passwordSalt,
                    RoleId = request.RoleId,
                };

                await _context.AddAsync(dbUser);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> Login(UserLogin request)
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

            return token;

        }
    }
}
