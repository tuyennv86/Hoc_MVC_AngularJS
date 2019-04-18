using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Tên đăng nhập không được bỏ trống!")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Mật khẩu không được bỏ trống!")]
        public string PassWord { get; set; }
        public bool RemembereMe { get; set; }
    }
}