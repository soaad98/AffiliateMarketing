using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AffiliateMarketing.Areas.Marchant.Models.ViewModels
{
    public class CampaignViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "عنوان الحملة")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "ملخص عن الحملة")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "وصف الحملة")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "عدد نقاط الحملة")]
        public int Point { get; set; }

        [Required]
        [Display(Name = "الصورة")]
        public IFormFile Img { get; set; }
    }
}
