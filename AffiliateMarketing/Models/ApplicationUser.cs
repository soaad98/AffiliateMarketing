using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AffiliateMarketing.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

        public string website { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int zip { get; set; }

        public Publisher Publisher { get; set; }

        public Marchant Marchant { get; set; }
    }
}
