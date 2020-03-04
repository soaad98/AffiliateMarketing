using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AffiliateMarketing.Models
{
    public class Marchant
    {
        public string Id { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
