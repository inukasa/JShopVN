using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JunShopVN.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "phone is required")]
        [Display(Name = "Phone")]
        public string phone { get; set; }
        [Required(ErrorMessage = "email is required")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Face")]
        public string facebook { get; set; }
        public RegisterUserModel(string users, string pass)
        {
            username = users;
            password = pass;
        }
        public RegisterUserModel()
        {

        }
    }

}