using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AffiliateMarketing.Models
{
    public class Publisher
    {
        public string Id { get; set; }
        public string FacebookUrl { get; set; }
        public string TwiterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string OtherUrl { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }

    }
}
