using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Address
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string OpenAddress { get; set; }
    }
}
