using System;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class ContacDetailViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Tên không được để trống!")]
        [MaxLength(256,ErrorMessage ="Tên không được vượt quá 256 ký tự!")]
        public string Name { get; set; }
             
        [MaxLength(50, ErrorMessage = "Điện thoại không được vượt quá 50 ký tự")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [MaxLength(256, ErrorMessage = "Email không được vượt quá 256 ký tự!")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email khống đúng định dạng!")]
        public string Email { get; set; }

        [MaxLength(256, ErrorMessage = "Witesite không được vượt quá 256 ký tự!")]        
        public string Website { get; set; }

        [MaxLength(256, ErrorMessage = "Địa chỉ không được vượt quá 256 ký tự!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Số thứ tự không được để trống!")]
        public int Order { get; set; }

        [Required(ErrorMessage = "Kinh độ không được để trống!")]        
        public string LatMap { get; set; }

        [Required(ErrorMessage = "Vĩ độ không được để trống!")]
        public string LngMap { get; set; }

        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}