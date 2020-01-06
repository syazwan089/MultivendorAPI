using MultiVendorAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiVendorAPI.Dtos
{
    public class UserToSend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string StripeKey { get; set; }
        public string Level { get; set; }
        public ICollection<Users> Agent { get; set; }
        public int MasterId { get; set; }
        public int StokisId { get; set; }
    }
}
