using MultiVendorAPI.Dtos;
using MultiVendorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiVendorAPI.Data
{
    public interface IAuth
    {
        Task<Users> Register(Users user, string password);

        Task<UserToSend> Login(string username, string password);

        Task<bool> UserExists(string username);


    }
}
