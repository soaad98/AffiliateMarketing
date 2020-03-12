using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AffiliateMarketing.Models
{
    public class UserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) {

            if (value == null)
                return false;

            var stringValue = value as string;

            if (stringValue == null)
                return false;

            //ErrorMessage = "يجب ان يحتوي الاسم على ارقام";

            //stringValue.Matcj
            return true;
        }
    }
}
