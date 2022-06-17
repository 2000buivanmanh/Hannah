using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DATA.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNguoiDung { get; set; }
        [Display(Name = "User Name")]
        public string TenDangNhap { get; set; }
        [Display(Name = "Email")]
        public string EmailNguoiDung { get; set; }
        [Display(Name = "Password")]
        public string MatKhau { get; set; }
        [Display(Name = "Full Name")]
        public string HoTen { get; set; }
        [Display(Name = "Address")]
        public string DiaChi { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime? NgaySinh { get; set; }
        [Display(Name = "Phone Number")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Accumulated points")]
        public int? DiemTichLuy { get; set; }
        [Display(Name = "Avatar")]
        public string AnhDaiDien { get; set; }
        [Display(Name = "Introduce")]
        public string GioiThieu { get; set; }
        [Display(Name = "Public Information")]
        public bool? CongKhaiThongTin { get; set; }
        [Display(Name = "User Status")]
        public bool? TinhTrangNguoiDung { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public DateTime? LanHoatDongCuoi { get; set; }
        [Display(Name = "User Role")]
        public int? VaiTro { get; set; }
        [Display(Name = "Status")]
        public int? TrangThai { get; set; }
        public string Capcha { get; set; }
        public DateTime? ThoiHanCapcha { get; set; }
        public int? SoLanGuiMa { get; set; }
        [Display(Name = "Referrer's name")]
        public string TenNguoiGioiThieu { get; set; }
        [Display(Name = "Referrer's phone number")]
        public string SDTNguoiGioiThieu { get; set; }
        [Display(Name = "Relationship")]
        public string MoiQuanHe { get; set; }
        [Display(Name = "Referrer's email")]
        public string EmailNguoiGioiThieu { get; set; }

        [NotMapped]
        public HttpPostedFileBase TaiHinhAnh { get; set; }
    }
}
