using AffiliateMarketing.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AffiliateMarketing.Models.Account.RegisterViewModels
{
    public class PubRegisterViewModel
    {

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم الهاتف")]
        public string phone { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "الموقع الالكتروني")]
        public string website { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Display(Name = "رابط الفيس")]
        [DataType(DataType.Url)]
        public string FacebookUrl { get; set; }
        [Display(Name = "رابط صفحة التويتر")]
        [DataType(DataType.Url)]
        public string TwiterUrl { get; set; }
        [Display(Name = "رابط صفحة الانستاغرام")]
        [DataType(DataType.Url)]
        public string InstagramUrl { get; set; }
        [Display(Name = "رابط قناة اليوتيوب")]
        [DataType(DataType.Url)]
        public string YoutubeUrl { get; set; }
        [Display(Name = "روابط اخرى")]
        [DataType(DataType.Url)]
        public string OtherUrl { get; set; }

        [Display(Name = "الدولة")]
        public string Country { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "zip code")]
        public int zip { get; set; }

        [UserName]
        [Display(Name = "اسم الشخصي")]
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "الايميل")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "كلمة المرور يجب ان تكون بين {1} - {0}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "غير متطابق")]
        public string ConfirmPassword { get; set; }

    }

    public class MarRegisterViewModel
    {
        [Required]
        [Display(Name = "اسم المنظمة")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "اسم التواصل")]
        public string ContactName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "رقم الهاتف")]
        public string phone { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "الموقع الالكتروني")]
        public string website { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Display(Name = "المدينة")]
        public string City { get; set; }

        [Display(Name = "الدولة")]
        public string Country { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "zip code")]
        public int zip { get; set; }

        [UserName]
        [Display(Name = "اسم الشخصي")]
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "الايميل")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "كلمة المرور يجب ان تكون بين {1} - {0}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "غير متطابق")]
        public string ConfirmPassword { get; set; }

    }

}
