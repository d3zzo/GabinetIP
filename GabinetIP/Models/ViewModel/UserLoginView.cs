using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GabinetIP.Models.ViewModel
{
    public class UserLoginView
    {
        [Key]
        public int SYSUserID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name ="Nazwa użytkownika")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}