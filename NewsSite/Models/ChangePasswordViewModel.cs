using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsSite.Models
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Old password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display (Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}