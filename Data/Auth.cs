using Microsoft.EntityFrameworkCore;
using MultiVendorAPI.Dtos;
using MultiVendorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MultiVendorAPI.Data
{
    public class Auth : IAuth
    {

        private readonly DataContext _context;
        public Auth(DataContext context)
        {
            _context = context;
        }


        public async Task<UserToSend> Login(string email, string password)
        {
            var users = await _context.users.FirstOrDefaultAsync(x => x.Email == email);

            if(users == null)
            {
                return null;
            }

            if(!VerifyPasswordHash(password,users.PasswordHash,users.PasswordSalt))
            {
                return null;
            }

            if(users.Status != "Approve")
            {
                return null;
            }

            UserToSend send = new UserToSend { Address = users.Address,
                Email = users.Email,
                Agent = users.Agent,
                Facebook = users.Facebook,
                Id = users.Id,
                Level = users.Level,
                Name = users.Name,
                Phone = users.Phone,
                StokisId = users.StokisId,
                StripeKey = users.StripeKey
            };

            return send;
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }


                }
            }

            return true;
        }


        public async Task<Users> Register(Users user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Status = "Pending";
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        public async Task<bool> UserExists(string email)
        {
            if (await _context.users.AnyAsync(x => x.Email == email))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
