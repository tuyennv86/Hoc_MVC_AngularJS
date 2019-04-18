using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Bạn phải nhập họ tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Bạn phải nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [MinLength(6,ErrorMessage ="Mật khẩu phải có ít nhất 6 ký tự")]
        public string PassWord { get; set; }
        [Required(ErrorMessage ="Bạn phải nhập Email")]
        [EmailAddress(ErrorMessage ="Địa chỉ Email không hợp lệ")]
        public string Email { get; set; }

        public string Address { get; set; }
        [Required(ErrorMessage ="Bạn phải nhập số điện thoại")]
        public string PhoneNumber { get; set; }

    }
}