using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiVendorAPI.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Phone { get; set; }
        public string StripeKey { get; set; }
        public string Level { get; set; }
        public ICollection<Users> Agent { get; set; }
        public int MasterId { get; set; }
        public int StokisId { get; set; }
    }
}
