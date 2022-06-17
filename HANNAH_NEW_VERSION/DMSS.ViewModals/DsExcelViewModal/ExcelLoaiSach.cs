using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
    [Table("ExcelLoaiSach")]
    public class ExcelLoaiSach : ViewModalDaDung
    {
        [Display(Name = "Name of the book")]
        public string TenLoaiSach { get; set; }
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Mota { get; set; }

    }
}
