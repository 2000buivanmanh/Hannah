using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
    [Table("ExcelNguoiDung")]
    public class ExcelNguoiDung : ViewModalDaDung
    {

        [Display(Name = "User Name")]
        public string TenDangNhap { get; set; }
        [Display(Name = "Email")]
        public string EmailNguoiDung { get; set; }
        [Display(Name = "Full Name")]
        public string HoTen { get; set; }

    }
}
