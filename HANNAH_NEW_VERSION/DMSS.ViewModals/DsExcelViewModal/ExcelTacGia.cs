using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
    [Table("ExcelTacGia")]
    public class ExcelTacGia : ViewModalDaDung
    {
        [Required]
        [Display(Name = "Author name")]
        [MaxLength(250)]
        public string TenTacGia { get; set; }
        [Display(Name = "Infomation")]
        [MaxLength(500)]
        public string ThongTinTacGia { get; set; }

    }
}
