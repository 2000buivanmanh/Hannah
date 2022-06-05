using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSS.ViewModals.DsExcelViewModal
{
    [Table("ExcelHangMucSach")]
    public class ExcelHangMucSach
    {
        [MaxLength(250)]
        [Display(Name = "Book Class Name")]
        public string TenHangMuc { get; set; }
        [MaxLength(50)]
        [Display(Name = "Book Class Code")]
        public string MaNhanDienHangMucSach { get; set; }
        [StringLength(500)]
        [Display(Name = "Description")]
        public string Mota { get; set; }
        [StringLength(500)]
        [Display(Name = "Meta Description")]
        public string NoiDungSeo { get; set; }
        [StringLength(500)]
        [Display(Name = "Meta Keywords")]
        public string TuKhoaSeo { get; set; }
        [StringLength(500)]
        [Display(Name = "Title Seo")]
        public string TieuDeSeo { get; set; }
        [StringLength(500)]
        [Display(Name = "Path Seo")]
        public string DuongDanSeo { get; set; }
        [Display(Name = "Notify")]
        public string ThongBao { get; set; }
        [Display(Name = "#")]
        public int? Stt { get; set; }
    }
}
