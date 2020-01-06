using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiVendorAPI.Models
{
    public class Master
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StripeKey { get; set; }
        public ICollection<Users> Stokis { get; set; }
    }
}
