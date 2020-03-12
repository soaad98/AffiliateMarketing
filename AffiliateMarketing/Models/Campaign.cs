using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace AffiliateMarketing.Models
{
    public class Campaign
    {
        public Guid Id{ get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User{ get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public int Point { get; set; }

        public string ImgUrl { get; set; }

    }
}
